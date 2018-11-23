import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LayoutComponent } from './layout/layout.component';



const pagesRoutes: Routes = [
  { path:'', 
    component: LayoutComponent,
    children: [
      { path: 'home', component: HomeComponent },
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
