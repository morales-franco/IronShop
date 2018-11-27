import { Injectable } from '@angular/core';
import { ServiceModule } from '../service.module';
import { IMenu } from 'src/app/models/IMenu';

@Injectable({
  providedIn: ServiceModule
})
export class SidebarService {

  public menu: IMenu[] = [{
    title : "Principal",
    icon : "mdi mdi-gauge",
    submenu : [
      { title : "Home", url : "/home" },
      { title : "Progress Test", url : "/progress" }
    ]
  }];

  constructor() { }
}
