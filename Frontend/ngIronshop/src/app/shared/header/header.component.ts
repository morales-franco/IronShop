import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user/user.service';
import { Profile } from '../../models/profile.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

  constructor(public userService: UserService) { }

  ngOnInit() {
  }

  logout(){
    this.userService.logout();
  }
}
