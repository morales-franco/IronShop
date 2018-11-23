import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styles: []
})

//MASTER PAGE
/*
LayoutComponent si bien podria estar en la carpeta shared lo declaramos en este modulo "PAGES" puesto que maneja el ruteo de las Pages
tiene un router-outlet que utiliza el PagesRoutingModule.
LayoutComponent soportara todas las pages.
LayoutComponent se construye a partir de los componentes del shared
 */

export class LayoutComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
