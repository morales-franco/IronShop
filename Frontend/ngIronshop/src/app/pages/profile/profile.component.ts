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

  constructor(private _userService: UserService,
   private _router: Router ) { }

  ngOnInit() {
    this.userId = this._userService.currentUser.userId;
    this.userFullName = this._userService.currentUser.fullName;
    this.userEmail = this._userService.currentUser.email;
    this.userRole = this._userService.currentUser.role;
    this.googleAuth = this._userService.currentUser.googleAuth;
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

}
