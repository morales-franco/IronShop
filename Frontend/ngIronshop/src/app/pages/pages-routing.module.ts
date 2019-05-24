import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { eRole } from '../models/eRole';

import { AuthenticationGuard } from '../guards/authentication.guard';
import { AuthorizationGuard } from '../guards/authorization.guard';

import { HomeComponent } from './home/home.component';
import { LayoutComponent } from './layout/layout.component';
import { ProgressComponent } from './progress/progress.component';
import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { PromesasComponent } from './promesas/promesas.component';
import { RxjsComponent } from './rxjs/rxjs.component';
import { ProfileComponent } from './profile/profile.component';
import { UsersComponent } from './users/users.component';
import { ProductIndexComponent } from './products/product-index/product-index.component';
import { ProductCreateComponent } from './products/product-create/product-create.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { OrdersComponent } from './store/orders/orders.component';
import { PurchaseComponent } from './store/purchase/purchase.component';




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
    canActivate: [AuthenticationGuard],
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
Create a child component-less and set canActivateChild: [AuthenticationGuard] instead of adding 
the  [AuthenticationGuard]  to each route individually.

canActivateChild check permission in child routes.
 */

const pagesRoutes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthenticationGuard],
    children: [
      {
        path: '',
        canActivateChild: [AuthenticationGuard],
        children: [
          { path: 'home', component: HomeComponent, data: { title: 'Home' } },
          { path: 'progress', component: ProgressComponent, data: { title: 'Progress' } },
          { path: 'account-settings', component: AccountSettingsComponent, data: { title: 'Configuración' } },
          { path: 'promesas', component: PromesasComponent, data: { title: 'Promesas' } },
          { path: 'rxjs', component: RxjsComponent, data: { title: 'RxJs - Observables' } },

          { path: 'profile', component: ProfileComponent, data: { title: 'profile' } },
          { path: 'users', component: UsersComponent, data: { title: 'users', roles: [ eRole.Admin ] }, canActivate: [ AuthorizationGuard ] },
          { path: 'products', component:ProductIndexComponent, data: { title : 'products' , roles: [ eRole.Admin, eRole.ProductManager ] }, canActivate: [ AuthorizationGuard ] },
          { path: 'product/create', component: ProductCreateComponent, data: { title : 'Product', roles: [ eRole.Admin, eRole.ProductManager ]  }, canActivate: [ AuthorizationGuard ] },
          { path: 'product/edit/:id', component: ProductEditComponent, data: { title : 'Product', roles: [ eRole.Admin, eRole.ProductManager ]  }, canActivate: [ AuthorizationGuard ] },
          { path: 'product/edit/:id', component: ProductEditComponent, data: { title : 'Product', roles: [ eRole.Admin, eRole.ProductManager ]  }, canActivate: [ AuthorizationGuard ] },
          { path: 'orders', component: OrdersComponent, data: { title : 'Orders', roles: [ eRole.Admin, eRole.SalesManager ]  }, canActivate: [ AuthorizationGuard ] },
          { path: 'purchase', component: PurchaseComponent, data: { title : 'Purchase', roles: [ eRole.Admin, eRole.SalesManager, eRole.Employee ]  }, canActivate: [ AuthorizationGuard ] },
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
