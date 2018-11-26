import { Component, OnInit, Input, EventEmitter, Output, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-incrementador',
  templateUrl: './incrementador.component.html',
  styles: []
})
export class IncrementadorComponent implements OnInit {
  @ViewChild('inputProgress') inputProgress : ElementRef;
  /* @FM:
  ViewChild hace referencia a un elemento html, el cúal taggeamos con #inputProgress, de 
  esta forma podemos acceder a sus propiedades
  */

  @Input('leyenda') title: string = "Titulo"
  @Input() progress: number = 50;

  /*@FM:
  Input: Son valores de entrada para el componente. La pagina que utiliza este componente puede
  pasarle:
  * un valor fijo
  * un valor variable
  * No pasarle nada y este caso toma el valor de inicialización

  <app-incrementador leyenda="Incrementador 1 (Azul)" [progress]="progress1" (onChangeProgress)="progress1 = $event"></app-incrementador>
<!--[progress]="50" Envia un number ; progress="50": Envia un string -->
   */

  @Output() onChangeProgress: EventEmitter<number> = new EventEmitter();

  /*@FM:
  Output: Son eventos que este componente emite para que otros componentes los escuchen
  (onChangeProgress)="progress1 = $event" 
  $event es obligatorio si se emite un evento que devuelve algo. 
   */

  constructor() { }

  ngOnInit() {
  }

  onManualChangeValue(value: number): void{
    if (this.progress >= 100 && value > 0)
      this.progress = 100;
    else if (this.progress <= 0 && value < 0)
      this.progress = 0;
    else
      this.progress =  value;

    this.inputProgress.nativeElement.value = this.progress;

    this.onChangeProgress.emit(this.progress);

  }

  changeValue(value: number): void {
    if (this.progress >= 100 && value > 0)
      return;

    if (this.progress <= 0 && value < 0)
      return;

    this.progress = this.progress + value;

    this.onChangeProgress.emit(this.progress);
    this.inputProgress.nativeElement.focus();

  }
}
