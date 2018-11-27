import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
declare function  initPlugins();
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

  constructor(public _router : Router) { }

  ngOnInit() {
    initPlugins();
  }

  login(){
    this._router.navigate(["/home"]);
  }

}
