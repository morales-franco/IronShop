import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user/user.service';
import { Profile } from '../../models/profile.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

   profile: Profile;

  constructor(private _userService: UserService) { }

  ngOnInit() {
    this.profile = this._userService.getCurrentUser();
  }

  logout(){
    this._userService.logout();
  }
}
