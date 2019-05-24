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
    public userService: UserService) { }

  ngOnInit() {
    this._sidebarService.setMenuByPermissions();
  }

  logout(){
    this.userService.logout();
  }

}
