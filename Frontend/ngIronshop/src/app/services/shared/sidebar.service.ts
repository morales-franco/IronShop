import { Injectable } from '@angular/core';
import { ServiceModule } from '../service.module';
import { Menu, Submenu } from '../../models/Menu';
import { UserService } from '../user/user.service';
import { Permission } from '../../models/permission.model';

@Injectable({
  providedIn: ServiceModule
})
export class SidebarService {

  public menu: Menu[] = new Array<Menu>();
  // public menu: IMenu[] = [
  //   {
  //     title : "Principal",
  //     icon : "mdi mdi-gauge",
  //     submenu : [
  //       { title : "Home", url : "/home" },
  //       { title : "Progress Test", url : "/progress" },
  //       { title : "Promesas", url : "/promesas" },
  //       { title : "Rxjs", url : "/rxjs" }
  //     ]
  //   },
  //   {
  //     title : "Configuration",
  //     icon : "mdi mdi-folder-lock-open",
  //     submenu : [
  //       { title : "Users", url : "/users" },
  //       { title : "Products", url : "/products" },
  //       { title : "Orders", url : "/orders" }
  //     ]
  //   }];

  constructor(private _UserService : UserService) { 
    this.setMenuByPermissions();
  }

  setMenuByPermissions(){
    let permissions = this._UserService.currentUser.permissions;

    debugger;
    if(permissions.length == 0)
      return;

    var menuEntries = permissions.filter(p => p.menuId !== null && p.display);

    if(menuEntries.length == 0)
      return;

    let menuIdDistinct = menuEntries.map(m => m.menuId)
                                    .filter((value, i, list) => {
                                      return list.indexOf(value) == i;
                                    })
                                    .sort((m1,m2) => {
                                      if(m1 < m2)
                                        return -1;
                                      else if( m1 > m2)
                                        return 1;
                                      
                                      return 0;
                                    } );

    menuIdDistinct.forEach(m => {
      let menuData : Permission[] = menuEntries.filter(p => p.menuId == m);

      let menuEntry : Menu = new Menu();
      menuEntry.displayName = menuData[0].menuDisplayName;
      menuEntry.icon = menuData[0].menuIcon;
      menuEntry.order = menuData[0].menuId;

      menuData.forEach(sm => {
        let subMenuEntry : Submenu = new Submenu();
        subMenuEntry.displayName = sm.displayName;
        subMenuEntry.url = sm.url;
        menuEntry.submenues.push(subMenuEntry);
      })

      this.menu.push(menuEntry);
      });


  }

}
