import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts';

import { HomeComponent } from './home/home.component';
import { SharedModule } from '../shared/shared.module';
import { PagesRoutingModule } from './pages-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { ProgressComponent } from './progress/progress.component';
import { IncrementadorComponent } from '../components/incrementador/incrementador.component';
import { ChartDoughnutComponent } from '../components/chart-doughnut/chart-doughnut.component';




@NgModule({
  declarations: [
    HomeComponent,
    LayoutComponent,
    ProgressComponent,
    IncrementadorComponent,
    ChartDoughnutComponent
  ],
  imports:[
    SharedModule,
    PagesRoutingModule,
    FormsModule,
    ChartsModule
  ],
  exports: [
    HomeComponent,
    LayoutComponent
  ]
})
export class PageModule { }
