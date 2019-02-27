import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user/user.service';
import { Profile } from '../../models/profile.model';
import { NgForm } from '@angular/forms';
import swal from "sweetalert";
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styles: []
})
export class ProfileComponent implements OnInit {

  private userId : number;
  userFullName : string;
  userEmail : string;
  userRole : string;
  googleAuth: boolean;
  picture: File;
  fileNamePicture : string;
  pictureTemp: string;

  constructor(private _userService: UserService,
   private _router: Router ) { }

  ngOnInit() {
    this.userId = this._userService.currentUser.userId;
    this.userFullName = this._userService.currentUser.fullName;
    this.userEmail = this._userService.currentUser.email;
    this.userRole = this._userService.currentUser.role;
    this.googleAuth = this._userService.currentUser.googleAuth;
    this.fileNamePicture = this._userService.currentUser.imageFileName;
  }

  updateProfile(profileForm: NgForm){

    if(profileForm.invalid){
      swal("Error", "Please, complete required fields.", "error");
      return;
    }

    let userModified: Profile = new Profile(
      this.userId,
      this.userFullName, 
      this.userEmail
    );

    this._userService.updateProfile(userModified)
        .subscribe(r => {
          swal("Success", "User was updated successfully!", "success");
          this._router.navigate(["/home"]);
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

  uploadProfile(){
    this._userService.updateProfilePicture(this.picture, this.userId)
      .then(resp => {
        swal("Success", "Picture was updated successfully!", "success");
      })
      .catch(resp => {
        swal("Error", "Can not upload picture!", "error");
      });
  }

}
