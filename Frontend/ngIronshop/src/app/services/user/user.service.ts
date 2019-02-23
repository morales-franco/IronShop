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


@Injectable({
  providedIn: ServiceModule
})
export class UserService {
  constructor(private _httpClient: HttpClient,
    private _sessionService: SessionService) {
    console.log("User service started!");
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
        map((ironToken : IronToken) => {
          this.saveCredentials(ironToken);
          return true;
        })
      );
  }
  
  loginGoogle(user: LoginGoogle){
    let url = `${environment.WEBAPI_ENDPOINT}/account/login/google`;

    return this._httpClient.post(url, user)
      .pipe(
        map((ironToken: IronToken) => {
          this.saveCredentials(ironToken);
          return true;
        }) 
      );

  }

  saveCredentials(ironToken: IronToken) {
    console.log(ironToken);
    this._sessionService.setItem(SessionConstants.IronToken, ironToken.token);
    this._sessionService.setObject(SessionConstants.User, ironToken.profile);
  }

  setRememberMe(rememberMe: boolean, userEmail: string) {
    if (rememberMe)
      this._sessionService.setItem(SessionConstants.RememberMe, userEmail);
    else
      this._sessionService.removeItem(SessionConstants.RememberMe);
  }

  getCurrentUser(): Profile{
    let user: Profile = this._sessionService.getObject(SessionConstants.User) || null;
    return user;
  }



}
