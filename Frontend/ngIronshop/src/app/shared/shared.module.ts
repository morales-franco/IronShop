import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FooterComponent } from './footer/footer.component';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { PreloaderComponent } from './preloader/preloader.component';
import { Error404Component } from './error/error404/error404.component';

/*
@FM:
RouterModule : Necesario para ruteo por ejemplo RouterLink
CommonModule: Necesario para utilizar sentencias comunes como ngFor

Si necesitaria formularios tendria que importar FormsModule que me permiten usar
ngModel o funciones como (ngSubmit)
*/

@NgModule({
  imports:[
    RouterModule,
    CommonModule
  ],
  declarations: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
    BreadcrumbComponent,
    PreloaderComponent,
    Error404Component
  ],
  exports: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
    BreadcrumbComponent,
    PreloaderComponent,
    Error404Component
  ]
})
export class SharedModule { }
