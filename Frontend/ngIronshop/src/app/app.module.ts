import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

//Modules
import { AppComponent } from './app.component';
import { PageModule } from './pages/pages.module';

//Module Services
import { ServiceModule } from './services/service.module';

//Router
import { AppRoutingModule } from './app-routing.module';


//Components
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';

/*
@FM:
Look at the module imports array. 
Notice that the AppRoutingModule is last. Most importantly, it comes after the PageModule.
AppRoutingModule: Siempre tiene que ir al final de las demas hojas de routeo
 */

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent
    
  ],
  imports: [
    BrowserModule,
    PageModule,
    AppRoutingModule,
    ServiceModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
