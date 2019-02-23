import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../user/user.service';

@Injectable({
  providedIn: 'root'
})
export class IronGuard implements CanActivate {

  constructor(private _userService : UserService,
    private _route: Router){
      console.log("Init Guard");
    
  }

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {

    if(!this._userService.existSessionActive())
    {
      console.log("Session does not exist");
      this._route.navigate(['/login'])
      return false;
    }

    console.log("Pass Iron Guard");
    return true;
  }
}
