import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler } from "@angular/common/http";
import { UserService } from "../services/user/user.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
 
  constructor(private _userService: UserService) {
  }
 
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    // Get the auth token from the service.
    const authToken = this._userService.getToken();
 
    // Clone the request and replace the original headers with
    // cloned headers, updated with the authorization.
    const authReq = req.clone({
      headers: req.headers.set('Authorization', `bearer ${authToken}`)
    });
 
    // send cloned request with header to the next handler.
    return next.handle(authReq);
  }
}