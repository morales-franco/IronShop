import { Injectable } from "@angular/core";
import { ServiceModule } from "../service.module";
import { Register } from "../../models/register.model";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment.prod";

@Injectable({
  providedIn: ServiceModule
})
export class UserService {
  constructor(private _httpClient: HttpClient) {
    console.log("User service started!");
  }

  register(userRegister: Register) {
    let url = `${environment.WEBAPI_ENDPOINT}/User`;

    return this._httpClient.post(url, userRegister);
  }
}
