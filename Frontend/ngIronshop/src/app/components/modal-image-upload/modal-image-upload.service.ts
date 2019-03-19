import { Injectable, EventEmitter } from '@angular/core';
import { ServiceModule } from '../../services/service.module';
import { UserService } from '../../services/user/user.service';
import swal from "sweetalert";
import { ProductService } from '../../services/product/product.service';

@Injectable({
  providedIn: ServiceModule
})
export class ModalImageUploadService {

  private type: string;
  private id: number;
  public hideCssClass : string = 'hide-modal';
  public uploadEvent = new EventEmitter<any>();

  constructor(private _UserService : UserService,
    private _productService: ProductService) { }

  hideModal(){
    this.hideCssClass = 'hide-modal';
  }

  showModal(id: number, type: string){
    this.hideCssClass = '';
    this.id = id;
    this.type = type;
  }

  uploadImage(picture: File): Promise<any>{
    
    if(this.type === "USER"){
      return this._UserService.updateProfilePicture(picture, this.id);
    }else{
      //PRODUCT
      return this._productService.updateImage(picture, this.id);
    }
  }


}
