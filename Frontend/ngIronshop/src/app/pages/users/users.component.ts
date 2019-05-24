import { Component, OnInit } from "@angular/core";
import { UserIndex } from "../../models/user.index.model";
import { UserService } from "../../services/user/user.service";
import swal from "sweetalert";
import { Profile } from '../../models/profile.model';
import { ModalImageUploadService } from '../../components/modal-image-upload/modal-image-upload.service';
import { Role } from '../../models/role.model';
import { eRole } from '../../models/eRole';

@Component({
  selector: "app-users",
  templateUrl: "./users.component.html",
  styles: []
})
export class UsersComponent implements OnInit {
  users: UserIndex[] = [];
  roles: Role[] = [];
  totalRows: number = 0;
  pageNumber: number = 1;
  rowsPerPage: number = 5;
  loading: boolean = false;

  constructor(private _userService: UserService,
    private _modalImageUploadService: ModalImageUploadService) {}

  ngOnInit() {
    this.loadGrid();
    this.loadRoles();

    this._modalImageUploadService.uploadEvent.subscribe(
      r => this.onUploadPictureSuccess(r),
      error => {
        swal("Error", "Internal Error, the picture was not updated.", "error");
      });
    
  }

  private loadRoles(){
    this.roles.push(new Role(eRole.Admin,"Admin"));
    this.roles.push(new Role(eRole.ProductManager,"Product Manager"));
    this.roles.push(new Role(eRole.SalesManager,"Sales Manager"));
    this.roles.push(new Role(eRole.Employee,"Employee"));
  }

  private onUploadPictureSuccess(user :any){
    swal("Success", "Operation successfully.", "success");
    this.loadGrid();
  }

  private loadGrid(name: string = null) {
    this.loading = true;
    this._userService
      .getList(this.rowsPerPage, this.pageNumber, "ASC", "fullname", name)
      .subscribe((u: any) => {
        this.totalRows = u.totalRows;
        this.users = u.rows;
        this.loading = false;
      });
  }

  goToPreviousPage() {
    if (this.pageNumber <= 1) {
      return;
    }

    this.pageNumber--;
    this.loadGrid();
  }

  goToNextPage() {
    let nextPage: number = this.pageNumber + 1;
    let maxPage: number = Math.ceil(this.totalRows / this.rowsPerPage);

    if (maxPage < nextPage) {
      return;
    }

    this.pageNumber++;
    this.loadGrid();
  }

  search(name: string) {
    if (name.length == 0) {
      this.loadGrid();
      return;
    }

    if (name.length < 3) {
      return;
    }

    this.loadGrid(name);
  }

  delete(user: UserIndex) {
    if (user.userId === this._userService.currentUser.userId) {
      swal("Error", "You don't remove yourself", "error");
      return;
    }

    swal({
      title: "Are you sure?",
      text: "Confirm delete user, please.",
      icon: "warning",
      buttons: ["cancel", "confirm"],
      dangerMode: true
    }).then(willDelete => {

      if (!willDelete) {
        return;
      }

      this.loading = true;

      this._userService.delete(user.userId)
        .subscribe(r=> {
          swal("Success", "User was deleted successfully.", "success");
          this.loadGrid();
        },
        error => {
          swal("Error", "Internal Error, user wasn't deleted.", "error");
          this.loading = false;
        });
    });
  }

  save(user: UserIndex) {
    swal({
      title: "Are you sure?",
      text: "Confirm the operation, please.",
      icon: "warning",
      buttons: ["cancel", "confirm"],
      dangerMode: true
    }).then(willUpdate => {

      if (!willUpdate) {
        return;
      }

      this.loading = true;

      let profile: Profile = new Profile
      (
        user.userId,
        user.fullName,
        user.email,
        user.roleId
      );

      var existRoleChanging : boolean = user.roleId != this._userService.currentUser.roleId;

      this._userService.updateProfile(profile)
        .subscribe(r=> {

          if(profile.userId == this._userService.currentUser.userId &&
            existRoleChanging){
            swal("Success", "User was updated successfully but you need to login again because you have changed the Role.", "success");
            this._userService.logout();
            return;
          }
          
          swal("Success", "User was updated successfully.", "success");
          this.loadGrid();
        },
        error => {
          swal("Error", "Internal Error, user wasn't updated.", "error");
          this.loading = false;
        });
    });
  }

  showModalUploadPicture(user: UserIndex){
    this._modalImageUploadService.showModal(user.userId, "USER");
  }

}


