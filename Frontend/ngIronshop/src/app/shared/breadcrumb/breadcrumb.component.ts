import { Component, OnInit } from '@angular/core';
import { Router, ActivationEnd } from '@angular/router';
import { filter, map } from 'rxjs/operators';
import { Title, MetaDefinition, Meta } from '@angular/platform-browser';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styles: []
})

/*
@FM:
Cambiar MetaTag en forma dinámica
1) Inyectar:private _meta : Meta (Meta '@angular/platform-browser';)
2)Configurar MetaDefinition (MetaDefinition '@angular/platform-browser';)
const metaTag : MetaDefinition = {
          name: 'description',
          content : data.title
        };
3) Actualizar MetaTag
this._meta.updateTag(metaTag);

-------
Cambiar Title en tiempo real
1) Inyectar: private _title : Title (Title '@angular/platform-browser';)
2) this._title.setTitle(TITULO_NUEVO);
*/

export class BreadcrumbComponent implements OnInit {

  public title : string;

  constructor(private _router : Router,
    private _title : Title,
    private _meta : Meta) { 

    this.getDataRoute()
      .subscribe(data => {
        console.log(data);
        this.title = data.title
        this._title.setTitle(data.title);

        const metaTag : MetaDefinition = {
          name: 'description',
          content : data.title
        };

        this._meta.updateTag(metaTag);

      });
  }

  ngOnInit() {

  }

  getDataRoute(){
    return this._router.events.pipe(
      filter(evento => evento instanceof ActivationEnd),
      filter((evento : ActivationEnd) => evento.snapshot.firstChild === null),
      map((evento : ActivationEnd) => evento.snapshot.data)
    )
  }

}
