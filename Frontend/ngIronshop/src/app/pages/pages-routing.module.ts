import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LayoutComponent } from './layout/layout.component';
import { ProgressComponent } from './progress/progress.component';
import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { PromesasComponent } from './promesas/promesas.component';
import { RxjsComponent } from './rxjs/rxjs.component';
import { IronGuard } from '../services/guard/iron.guard';




const pagesRoutes: Routes = [
  { path:'', 
    component: LayoutComponent,
    canActivate: [IronGuard],
    children: [
      { path: 'home', component: HomeComponent, data : { title : 'Home'} },
      { path: 'progress', component: ProgressComponent, data : { title : 'Progress'} },
      { path: 'account-settings', component: AccountSettingsComponent, data : { title : 'Configuración'} },
      { path: 'promesas', component: PromesasComponent, data : { title : 'Promesas'} },
      { path: 'rxjs', component: RxjsComponent, data : { title : 'RxJs - Observables'} },
      { path: '', redirectTo: '/home', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  declarations: [],
  exports: [
    RouterModule 
  ],
  imports: [ 
    RouterModule.forChild(pagesRoutes) 
  ],
})
export class PagesRoutingModule { }
