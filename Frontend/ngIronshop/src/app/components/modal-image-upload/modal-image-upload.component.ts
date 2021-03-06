import { Component, OnInit } from '@angular/core';
import { ModalImageUploadService } from './modal-image-upload.service';

@Component({
  selector: 'app-modal-image-upload',
  templateUrl: './modal-image-upload.component.html',
  styles: []
})

export class ModalImageUploadComponent implements OnInit {
  picture: File;
  pictureTemp: string;

  constructor(public modalUploadService: ModalImageUploadService) { 

  }

  ngOnInit() {
  }

  hideModal(){
    this.picture = null;
    this.pictureTemp = null;
    this.modalUploadService.hideModal();
  }

  uploadPicture(){
    this.modalUploadService.uploadImage(this.picture)
      .then(resp => {
        swal("Success", "Picture was updated successfully!", "success");
        this.modalUploadService.uploadEvent.emit(resp);
        this.hideModal();
      })
      .catch(resp => {
        swal("Error", "Can not upload picture!", "error");
      });
  }

  onSelectPicture(picture: File){

    if(!picture){
      this.picture = null;
      return;
    }

    if(picture.type.indexOf("image") < 0){
      swal("Error", "Can upload only images!", "error");
      this.picture = null;
      return;
    }
    this.picture = picture;

    let reader = new FileReader();
    reader.readAsDataURL(picture);

    reader.onloadend = () =>this.pictureTemp = reader.result.toString(); 

  }

}
