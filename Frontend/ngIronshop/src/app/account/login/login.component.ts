import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Login } from '../../models/login.model';
import { SessionService } from '../../services/session/session.service';
import { SessionConstants } from '../../commons/constants/session.constants';
import { UserService } from '../../services/user/user.service';
import { GoogleApi } from '../../commons/membership/google.api.membership';
import { LoginGoogle } from '../../models/login-google.model';

//@FM: Reference to JS functioninitPlugins() in assets/js/custom.js
declare function initPlugins();
//@FM: Reference to Google Library specified in index.html
declare const gapi:any;
/*
@FM:
Invocamos a la función InitPlugins que esta en assets/js/custom.js  
cuando se genera este componente. Esta función inicializa los plugins.
Luego la llamamos desde el layout (master page) cuando redireccionamos al home ya que hay se 
debe volver a ejecutar para inicializar entre otras cosas el sidebar 

declare function  initPlugins();
ngOnInit() {
    initPlugins();
  }

*/

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  auth2 : any;
  rememberMe: boolean = false;
  email: string = "";

  constructor(private _router: Router,
    private _userService: UserService,
    private _sessionService: SessionService) { }

  ngOnInit() {
    initPlugins();
    this.initGoogle();
    this.setDefaultValues();
  }

  initGoogle(){

    gapi.load('auth2', () => {
      this.auth2 = gapi.auth2.init({
        client_id : GoogleApi.ClientId,
        cookiepolicy: "single_host_origin",
        scope: "profile email"
      });

      this.attachSignin(document.getElementById('btn-google-id'))

    });
  }

  attachSignin(element){
    this.auth2.attachClickHandler(element, {}, ( googleUser ) => {
      //let profile = googleUser.getBasicProfile();
      let token = googleUser.getAuthResponse().id_token;
      let loginGoogle: LoginGoogle = new LoginGoogle(token);

      //window.location.href = Fix for google login
      this._userService.loginGoogle(loginGoogle)
        .subscribe(resp => window.location.href='/home')
    });
  }

  setDefaultValues() {
    //@FM: if getItem() return undefined then return "" --> getItem() || ""
    this.email = this._sessionService.getItem(SessionConstants.RememberMe) || "";

    if (this.email.length > 0)
      this.rememberMe = true;
  }

  login(loginForm: NgForm) {
    let userLogin: Login = new Login(
      loginForm.value.email,
      loginForm.value.password
    );

    this._userService.login(userLogin, loginForm.value.rememberMe)
      .subscribe(u => this._router.navigate(['/home']));
  }

}
