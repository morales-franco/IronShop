import { Component, OnInit } from '@angular/core';
import { SidebarService } from 'src/app/services/shared/sidebar.service';
import { UserService } from '../../services/user/user.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styles: []
})
export class SidebarComponent implements OnInit {

  constructor(public _sidebarService : SidebarService,
    private _userService: UserService) { }

  ngOnInit() {
    
  }

  logout(){
    this._userService.logout();
  }

}
