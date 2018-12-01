import { Component, OnInit } from '@angular/core';
declare function  initPlugins();

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styles: []
})


/*
@FM:
MASTER PAGE
LayoutComponent si bien podria estar en la carpeta shared lo declaramos en este modulo "PAGES" puesto que maneja el ruteo de las Pages
tiene un router-outlet que utiliza el PagesRoutingModule.
LayoutComponent soportara todas las pages.
LayoutComponent se construye a partir de los componentes del shared

initPlugins es una función que se encuentra en assets/js/custom.js que se encarga de inicializar todods
los plugins de la app.
cuando somos redireccionados del login esa función NO se llama, por default solo se llamaba cuando arranca
la app en el login por lo tanto cuando llegaba el layout por ejemplo el sidebar NO estaba y por lo tanto
NO le aplicaba los estilos necesarios. 
De esta forma invocamos a una función JS desde Angular
declare function  initPlugins();
ngOnInit() {
    initPlugins();
}
 */

export class LayoutComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    initPlugins();
  }

}
