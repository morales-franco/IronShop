import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { Error404Component } from './shared/error/error404/error404.component';
import { LayoutComponent } from './shared/layout/layout.component';
import { LoginComponent } from './pages/account/login/login.component';
import { RegisterComponent } from './pages/account/register/register.component';

/*
Estas son rutas principales cuando solamente tenes un router outlet.

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', component: Error404Component }
];

 * { path: '', redirectTo: '/home', pathMatch: 'full' } : Default Route
 * "**":  wildcard: The router will select this route if the requested URL doesn't match any paths for routes defined earlier in the configuration
 
 En nuestro caso vamos a utilizar 2
1) app.component: Router Outlet Principal. Ruteo de rutas principales en este caso tenemos 2: Layout y Login
2) app.layout: Router Outlet Child, mantiene layout como una master una para todas las rutas hijas del Layout

 */

const routes: Routes = [
  { path:'', 
    component: LayoutComponent,
    children: [
      { path: 'home', component: HomeComponent },
      { path: '', redirectTo: '/home', pathMatch: 'full' }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', component: Error404Component }
];

/*
default route sigue siendo home: cuando la app levanta automaticamente reconoce la default route y levanta 
el home que es soportado por el layout.

si ponemos cualquier ruta en el browser que no es reconocida por el archivo de ruteo entonces redirige al
Error404Component, aqui levanta el html asociado que NO conoce el Layout. login, register y error404 son independientes.

 */
@NgModule({
  declarations: [],
  exports: [
    RouterModule 
  ],
  imports: [ 
    RouterModule.forRoot(routes) 
  ],
})
export class AppRoutingModule { }
