import { Component, OnInit, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/platform-browser';
import { SettingsService } from 'src/app/services/settings/settings.service';

@Component({
  selector: 'app-account-settings',
  templateUrl: './account-settings.component.html',
  styles: []
})
export class AccountSettingsComponent implements OnInit {

  /*
  @FM:
  @Inject(DOCUMENT) Referencia a TODO el DOM
   */
  constructor(@Inject(DOCUMENT) private _document, 
    private _settingsService : SettingsService) { }

  ngOnInit() {
    this.checkCurrentTheme();
  }

  changeTheme(theme: string, link: any): void {
    this.applyCheck(link);
    this._settingsService.setThemeColor(theme);
  }

  private applyCheck(link: any) {
    let selectores: any = this._document.getElementsByClassName('selector');

    for (let selector of selectores) {
      selector.classList.remove('working');
    }

    link.classList.add('working');
  }

  private checkCurrentTheme() {
    let selectores: any = this._document.getElementsByClassName('selector');
    let currentTheme : string = this._settingsService.getCurrentTheme();

    for (let selector of selectores) {
      if(selector.getAttribute('data-theme') === currentTheme){
        selector.classList.add('working');
        break;
      }
    }
  }



}
