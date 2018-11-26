import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

//Modules
import { AppComponent } from './app.component';
import { PageModule } from './pages/pages.module';


//Router
import { AppRoutingModule } from './app-routing.module';


//Components
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';


/*
@FM:
Look at the module imports array. 
Notice that the AppRoutingModule is last. Most importantly, it comes after the PageModule.
AppRoutingModule: Siempre tiene que ir al final
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
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
