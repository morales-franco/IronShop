import { Injectable, EventEmitter } from '@angular/core';
import { ServiceModule } from '../../services/service.module';
import { UserService } from '../../services/user/user.service';
import swal from "sweetalert";

@Injectable({
  providedIn: ServiceModule
})
export class ModalImageUploadService {

  private type: string;
  private id: number;
  public hideCssClass : string = 'hide-modal';
  public uploadEvent = new EventEmitter<any>();

  constructor(private _UserService : UserService) { }

  hideModal(){
    this.hideCssClass = 'hide-modal';
  }

  showModal(id: number, type: string){
    this.hideCssClass = '';
    this.id = id;
    this.type = type;
  }

  uploadImage(picture: File): Promise<any>{
    return this._UserService.updateProfilePicture(picture, this.id);
    // if(this.type === "USER"){
      
    // }
  }


}
