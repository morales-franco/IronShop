import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { Error404Component } from './shared/error/error404/error404.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', component: Error404Component }
];

/**
 * { path: '', redirectTo: '/home', pathMatch: 'full' } : Default Route
 * "**":  wildcard: The router will select this route if the requested URL doesn't match any paths for routes defined earlier in the configuration
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
