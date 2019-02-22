import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

/*
@FM: Generate Module
ng g m services/service --no--spec --flat

Agrupamos todos los servicios en este module y luego los referenciamos desde el app.module principal.
Aqui NO estamos declarando los providers debido a que cada Servicio referencia a su module, en este caso
en cada servicio que generamos especificamos:
@Injectable({
  providedIn: ServiceModule --> Declaramos MODULE que utiliza este servicio
})
export class SettingsService {

Equivalente a esto seria:

@Injectable()
export class SettingsService {

  y luego en ServiceModule:

  @NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [SettingsService]
})
  
*/

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ]
})
export class ServiceModule { }
