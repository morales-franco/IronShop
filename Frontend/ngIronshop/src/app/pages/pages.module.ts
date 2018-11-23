import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { SharedModule } from '../shared/shared.module';
import { PagesRoutingModule } from './pages-routing.module';
import { LayoutComponent } from './layout/layout.component';



@NgModule({
  declarations: [
    HomeComponent,
    LayoutComponent
  ],
  imports:[
    SharedModule,
    PagesRoutingModule
  ],
  exports: [
    HomeComponent,
    LayoutComponent
  ]
})
export class PageModule { }
