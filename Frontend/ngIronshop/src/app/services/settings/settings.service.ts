import { Injectable, Inject } from '@angular/core';
import { ISettings } from '../../models/ISettings';
import { DOCUMENT } from '@angular/platform-browser';
import { ServiceModule } from '../service.module';

/*
@FM:
Generar servicios mediante angular CLI
ng g s services/settings  --no--spec
 providedIn: 'root' es lo mismo que especificar un provider en el root: app.module
 si quisieramos que este otro module:
 providedIn: ServiceModule
 */
@Injectable({
  providedIn: ServiceModule
})
export class SettingsService {

  private _settings: ISettings = {
    theme: 'default',
    themeUrl: 'assets/css/colors/default.css'
  };

  constructor(@Inject(DOCUMENT) private _document) {
    this.loadSettings();
   }

  private saveSettings() {
    localStorage.setItem('settings', JSON.stringify(this._settings));
  }

  loadSettings() {
    if (localStorage.getItem('settings')) {
      //Si existe una configuración en Storage la traigo
      this._settings = JSON.parse(localStorage.getItem('settings'));
    }

    //Cargo tema default o en su defecto lo que tnia el usuario en el storage
    this.applyTheme();

  }

  setThemeColor(theme: string) {
    this._settings.theme = theme;
    this._settings.themeUrl = `assets/css/colors/${theme}.css`;
    this.saveSettings();
    this.applyTheme();

  }

  /*
  @FM:
  Cambiar Theme en forma dinamica
   */
  private applyTheme(): void {
    this._document.getElementById('themeId').setAttribute('href', this._settings.themeUrl);
  }

  getCurrentTheme(): string {
    return this._settings.theme;
  }


}
