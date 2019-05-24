import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ServiceModule } from '../service.module';
import { Product } from '../../models/product.model';
import { UserService } from '../user/user.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

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

    return this._httpClient.get(url).pipe(
      catchError(this.handleError<Product>(url))
    );
  }

  getById(productId : number): Observable<Product>{
    const url = `${environment.WEBAPI_ENDPOINT}/product/${productId}`;
    
    return this._httpClient.get<Product>(url).pipe(
      catchError(this.handleError<Product>(`getProductById id=${productId}`))
    );
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


  create(product: Product): Observable<Product> {
    const url = `${environment.WEBAPI_ENDPOINT}/product`;

    return this._httpClient.post<Product>(url, product)
      .pipe(catchError(this.handleError<Product>('create')))
  }


  update (product: Product): Observable<Product> {
    const url = `${environment.WEBAPI_ENDPOINT}/product/${product.productId}`;
    return this._httpClient.put<Product>(url, product)
      .pipe(
        catchError(this.handleError('update Product', product))
      );
  }

  delete (productId: number): Observable<any> {
    const url = `${environment.WEBAPI_ENDPOINT}/product/${productId}`;

  
    return this._httpClient.delete<any>(url).pipe(
      catchError(this.handleError<any>('deleteProduct'))
    );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); 
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

}
