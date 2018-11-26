import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { HomeComponent } from './home/home.component';
import { SharedModule } from '../shared/shared.module';
import { PagesRoutingModule } from './pages-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { ProgressComponent } from './progress/progress.component';
import { IncrementadorComponent } from '../components/incrementador/incrementador.component';




@NgModule({
  declarations: [
    HomeComponent,
    LayoutComponent,
    ProgressComponent,
    IncrementadorComponent
  ],
  imports:[
    SharedModule,
    PagesRoutingModule,
    FormsModule
  ],
  exports: [
    HomeComponent,
    LayoutComponent
  ]
})
export class PageModule { }
