import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LayoutComponent } from './layout/layout.component';
import { ProgressComponent } from './progress/progress.component';
import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { PromesasComponent } from './promesas/promesas.component';
import { RxjsComponent } from './rxjs/rxjs.component';
import { ProfileComponent } from './profile/profile.component';
import { AuthGuard } from '../guards/auth.guard';
import { UsersComponent } from './users/users.component';

/*
@FM:canActivateChild
This version check AuthGuard when user access to the module.
canActivate only check permission one time. 
No check when user routing throw children routes.
Example Login --> home --> canActivate check!
home --> progress --> canActivate NO check!

1)In this case if I login to the app --> I have a token in local storage.
2)login --> home --> pass it! --> save token
3)user clear local storage in browser
4)home --> progress --> NO problem AuthGuard No check permission again!


const pagesRoutes: Routes = [
  { path:'', 
    component: LayoutComponent,
    canActivate: [AuthGuard],
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

Solution:
Create a child component-less and set canActivateChild: [AuthGuard] instead of adding 
the  [AuthGuard]  to each route individually.

canActivateChild check permission in child routes.
 */

const pagesRoutes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        canActivateChild: [AuthGuard],
        children: [
          { path: 'home', component: HomeComponent, data: { title: 'Home' } },
          { path: 'progress', component: ProgressComponent, data: { title: 'Progress' } },
          { path: 'account-settings', component: AccountSettingsComponent, data: { title: 'Configuración' } },
          { path: 'promesas', component: PromesasComponent, data: { title: 'Promesas' } },
          { path: 'rxjs', component: RxjsComponent, data: { title: 'RxJs - Observables' } },

          { path: 'profile', component: ProfileComponent, data: { title: 'Profile' } },
          { path: 'users', component: UsersComponent, data: { title: 'Users' } },

          { path: '', redirectTo: '/home', pathMatch: 'full' }
        ]
      }]
    }];

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
