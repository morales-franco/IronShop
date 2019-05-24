import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts';
import { PipesModule } from '../pipes/pipes.module';
import { PagesRoutingModule } from './pages-routing.module';

import { HomeComponent } from './home/home.component';
import { SharedModule } from '../shared/shared.module';
import { LayoutComponent } from './layout/layout.component';
import { ProgressComponent } from './progress/progress.component';
import { IncrementadorComponent } from '../components/incrementador/incrementador.component';
import { ChartDoughnutComponent } from '../components/chart-doughnut/chart-doughnut.component';
import { AccountSettingsComponent } from './account-settings/account-settings.component';
import { PromesasComponent } from './promesas/promesas.component';
import { RxjsComponent } from './rxjs/rxjs.component';
import { ProfileComponent } from './profile/profile.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../interceptors/auth.interceptor';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users/users.component';
import { ModalImageUploadComponent } from '../components/modal-image-upload/modal-image-upload.component';
import { ProductIndexComponent } from './products/product-index/product-index.component';
import { ProductCreateComponent } from './products/product-create/product-create.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';

//https://github.com/cesarrew/ng2-currency-mask
import { CurrencyMaskModule } from "ng2-currency-mask";
import { OrdersComponent } from './store/orders/orders.component';
import { PurchaseComponent } from './store/purchase/purchase.component';

@NgModule({
  declarations: [
    HomeComponent,
    LayoutComponent,
    ProgressComponent,
    IncrementadorComponent,
    ChartDoughnutComponent,
    AccountSettingsComponent,
    PromesasComponent,
    RxjsComponent,
    ProfileComponent,
    UsersComponent,
    ModalImageUploadComponent,
    ProductIndexComponent,
    ProductCreateComponent,
    ProductEditComponent,
    OrdersComponent,
    PurchaseComponent
  ],
  imports:[
    SharedModule,
    PagesRoutingModule,
    FormsModule,
    ChartsModule,
    PipesModule,
    CommonModule,
    CurrencyMaskModule
  ],
  exports: [
    HomeComponent,
    LayoutComponent
  ],
  providers:[
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ]
})
export class PageModule { }
