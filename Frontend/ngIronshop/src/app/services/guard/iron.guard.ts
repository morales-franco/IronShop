import { Injectable } from '@angular/core';
import { CanActivate, Router, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../user/user.service';

@Injectable({
  providedIn: 'root'
})
export class IronGuard implements CanActivate, CanActivateChild {
  

  constructor(private _userService : UserService,
    private _route: Router){
  }

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {
    if(!this._userService.existSessionActive())
    {
      console.log("Session does not exist");
      this._route.navigate(['/login'])
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
