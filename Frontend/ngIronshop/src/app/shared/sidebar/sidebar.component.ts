import { Component, OnInit } from '@angular/core';
import { SidebarService } from 'src/app/services/shared/sidebar.service';
import { UserService } from '../../services/user/user.service';
import { Profile } from '../../models/profile.model';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styles: []
})
export class SidebarComponent implements OnInit {

  profile: Profile;
  
  constructor(public _sidebarService : SidebarService,
    private _userService: UserService) { }

  ngOnInit() {
    this.profile = this._userService.getCurrentUser();
  }

  logout(){
    this._userService.logout();
  }

}
