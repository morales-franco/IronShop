import { Injectable } from "@angular/core";
import { ServiceModule } from "../service.module";
import { Register } from "../../models/register.model";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment.prod";
import { Login } from '../../models/login.model';
import { SessionService } from '../session/session.service';
import { SessionConstants } from '../../commons/constants/session.constants';
import { map } from "rxjs/operators";
import { LoginGoogle } from '../../models/login-google.model';
import { Profile } from '../../models/profile.model';
import { IronToken } from '../../models/iron-token.model';
import { Router } from '@angular/router';


@Injectable({
  providedIn: ServiceModule
})
export class UserService {
  /*
  @FM:Profile Variable
  It's necessary because when I update Profile, I need to reference currentUser in 
  sidebar & header for update email & name inmmediatly.
  I can't use this way:
  auxUser: Profile = _userService.currentUser;
  and reference auxUser in sidebar & header. 
  This way not refresh auxUser variable when we update the profile, 
  however _userService.currentUser has been updated.
  */
  currentUser: Profile;

  constructor(private _httpClient: HttpClient,
    private _sessionService: SessionService,
    private _router : Router) {
      this.currentUser = this.getCurrentUser();
  }

  register(userRegister: Register) {
    let url = `${environment.WEBAPI_ENDPOINT}/account/register`;

    return this._httpClient.post(url, userRegister);
  }

  login(user: Login, rememberMe: boolean) {
    let url = `${environment.WEBAPI_ENDPOINT}/account/login`;

    this.setRememberMe(rememberMe, user.email);

    return this._httpClient.post(url, user)
      .pipe(
        map((ironToken: IronToken) => {
          this.saveCredentials(ironToken);
          return true;
        })
      );
  }

  loginGoogle(user: LoginGoogle) {
    let url = `${environment.WEBAPI_ENDPOINT}/account/login/google`;

    return this._httpClient.post(url, user)
      .pipe(
        map((ironToken: IronToken) => {
          this.saveCredentials(ironToken);
          return true;
        })
      );

  }

  private saveCredentials(ironToken: IronToken) {
    this._sessionService.setItem(SessionConstants.IronToken, ironToken.token);
    this._sessionService.setItem(SessionConstants.ExpirationToken, ironToken.expiration.toString());
    this.updateProfileInSession(ironToken.profile);
  }

  private updateProfileInSession(profile: Profile){
    this._sessionService.setObject(SessionConstants.User, profile);
    this.currentUser = profile;
  }

  private setRememberMe(rememberMe: boolean, userEmail: string) {
    if (rememberMe)
      this._sessionService.setItem(SessionConstants.RememberMe, userEmail);
    else
      this._sessionService.removeItem(SessionConstants.RememberMe);
  }

  private getCurrentUser(): Profile {
    let user: Profile = this._sessionService.getObject(SessionConstants.User) || null;
    return user;
  }

  getToken(): string {
    let token: string = this._sessionService.getItem(SessionConstants.IronToken) || null;
    return token;
  }

  getExpirationToken(): Date {
    let expiration: string = this._sessionService.getItem(SessionConstants.ExpirationToken) || null;
    return expiration == null ? null : new Date(expiration);
  }

  existSessionActive() {
    let token: string = this.getToken();
    let currentUser: Profile = this.getCurrentUser();
    let expiration: Date = this.getExpirationToken();
    return currentUser != null && token != null && expiration != null && expiration > new Date(Date.now());

  }

  logout(){
    this._sessionService.removeItem(SessionConstants.IronToken);
    this._sessionService.removeItem(SessionConstants.ExpirationToken);
    this._sessionService.removeItem(SessionConstants.User);
    this._router.navigate(['/login']);
  }

  updateProfile(user: Profile){
    let url = `${environment.WEBAPI_ENDPOINT}/user/Profile/${user.userId}`;

    return this._httpClient.put(url, user)
      .pipe(
        map((result:any) => {
          let currentProfile : Profile = this.getCurrentUser();

          if(currentProfile.userId == user.userId){
            currentProfile.email = user.email;
            currentProfile.fullName = user.fullName;
            currentProfile.roleId = user.roleId;
            currentProfile.role = user.role;
            this.updateProfileInSession(currentProfile);
          }

          return true;
        })
      );
  }

  updateProfilePicture(file: File, userId: number){
    return new Promise((resolve, reject) => {
      let formData = new FormData();
      let xhr = new XMLHttpRequest();
  
      formData.append("file", file, file.name);
      xhr.onreadystatechange = () =>{
  
        if(xhr.readyState === 4){
          if(xhr.status === 200){
            let response: Profile = JSON.parse(xhr.response);

            if(userId === this.currentUser.userId){
              this.updateProfileInSession(response);
            }
            
            resolve(response);
          }else{
            console.log(JSON.parse(xhr.response));
            reject();
          }
        }
      };

    let url = `${environment.WEBAPI_ENDPOINT}/user/UploadProfilePicture/${userId}`;
    
    xhr.open('PUT', url, true);
    xhr.setRequestHeader('Authorization', 'Bearer ' + this.getToken());
    xhr.send(formData);
    });
  }

  getList(pageSize: number, pageNumber: number, dir: string, sort: string, fullName: string=null, email: string=null){
    let url = `${environment.WEBAPI_ENDPOINT}/User/GetAllPagedSP?pageSize=${pageSize}&pageNumber=${pageNumber}&dir=${dir}&sort=${sort}`;
    
    if(fullName != null){
      url+= `&fullName=${fullName}`
    }

    if(email != null){
      url+= `&fullName=${email}`
    }

    return this._httpClient.get(url);
  }

  delete(userId: number){
    let url = `${environment.WEBAPI_ENDPOINT}/User/${userId}`;
    return this._httpClient.delete(url);
  }

}
