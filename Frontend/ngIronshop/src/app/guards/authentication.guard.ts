import { Injectable } from '@angular/core';
import { CanActivate, Router, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate, CanActivateChild {
  /*
    Verify that the user is authenticated! 
  */

  constructor(private _userService : UserService,
    private _route: Router){
  }

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {
    if(!this._userService.existSessionActive())
    {
      this._userService.logout();
      return false;
    }
    return true;
  }

  canActivateChild(
    childRoute: ActivatedRouteSnapshot, 
    state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    return this.canActivate();

  }
}
