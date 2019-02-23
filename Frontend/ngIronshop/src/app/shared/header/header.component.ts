import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

  constructor(private _userService: UserService) { }

  ngOnInit() {
  }

  logout(){
    this._userService.logout();
  }
}
