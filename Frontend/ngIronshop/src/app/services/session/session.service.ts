import { Injectable } from '@angular/core';
import { ServiceModule } from '../service.module';

@Injectable({
  providedIn: ServiceModule
})
export class SessionService {

  constructor() { }

  setItem(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  removeItem(key: string) {
    localStorage.removeItem(key);
  }

  getItem(key: string){
    return localStorage.getItem(key);
  }

  setObject(key: string, object : any){
    this.setItem(key, JSON.stringify(object));
  }

  getObject(key: string){
    let valueObject : string = this.getItem(key);
    return JSON.parse(valueObject);
  }

}
