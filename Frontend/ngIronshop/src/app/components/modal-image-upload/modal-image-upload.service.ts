import { Injectable, EventEmitter } from '@angular/core';
import { ServiceModule } from '../../services/service.module';
import { UserService } from '../../services/user/user.service';
import swal from "sweetalert";

@Injectable({
  providedIn: ServiceModule
})
export class ModalImageUploadService {

  public type: string;
  public id: number;
  public hideCssClass : string = 'hide-modal';
  public uploadNotify = new EventEmitter<any>();

  constructor(private _UserService : UserService) { }

  hideModal(){
    this.hideCssClass = 'hide-modal';
  }

  showModal(id: number, type: string){
    this.hideCssClass = '';
    this.id = id;
    this.type = type;
  }

  uploadImage(picture: File){
    return this._UserService.updateProfilePicture(picture, this.id);
    // if(this.type === "USER"){
      
    // }
  }


}
