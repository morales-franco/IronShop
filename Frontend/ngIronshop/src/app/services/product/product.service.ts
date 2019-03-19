import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ServiceModule } from '../service.module';
import { Product } from '../../models/product.model';
import { UserService } from '../user/user.service';

@Injectable({
  providedIn: ServiceModule
})
export class ProductService {

  constructor(private _httpClient: HttpClient,
    private _userService: UserService) { }

  getList(pageSize: number, pageNumber: number, dir: string, sort: string, title: string=null, category: string=null){
    let url = `${environment.WEBAPI_ENDPOINT}/product/page?pageSize=${pageSize}&pageNumber=${pageNumber}&dir=${dir}&sort=${sort}`;
    
    if(title != null){
      url+= `&title=${title}`
    }

    if(category != null){
      url+= `&category=${category}`
    }

    return this._httpClient.get(url);
  }

  updateImage(file: File, productId: number){
    return new Promise((resolve, reject) => {
      let formData = new FormData();
      let xhr = new XMLHttpRequest();
  
      formData.append("file", file, file.name);
      xhr.onreadystatechange = () =>{
  
        if(xhr.readyState === 4){
          if(xhr.status === 200){
            let response: Product = JSON.parse(xhr.response);
            resolve(response);
          }else{
            console.log(JSON.parse(xhr.response));
            reject();
          }
        }
      };

    let url = `${environment.WEBAPI_ENDPOINT}/product/image/${productId}`;
    
    xhr.open('PUT', url, true);
    xhr.setRequestHeader('Authorization', 'Bearer ' + this._userService.getToken());
    xhr.send(formData);
    });
  }

}
