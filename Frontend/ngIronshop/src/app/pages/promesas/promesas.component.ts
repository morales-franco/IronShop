import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-promesas',
  templateUrl: './promesas.component.html',
  styles: []
})
export class PromesasComponent implements OnInit {

  public contador : number = 0;
  public state : string = "No!"
  public state2 : boolean = false
  
  /*
  @FM:
  setInterval: Repite la función cada 1 segundo 

  let intervalo = setInterval( () => {
    clearInterval(intervalo); --> Finalizar el intervalo, no repite más el interval
  }, 1000);

  Promise: se ejecuta la promesa y dentro del cuerpo de la misma se llama a reject() o resolve()
  luego se espera la finalziación de la promesa.
  let promesa = new Promise((resolve, reject) => {
    resolve("Promesa Exitosa!");
    // reject("Error en la promesa");
  });

  promesa.then(
      // () => this.state = "Termino!"
      result => this.state = JSON.stringify(result)
    ).catch(error => this.state = error);

   */
  constructor() { 
    let promesa = new Promise((resolve, reject) => {
      
      let intervalo = setInterval( () => {
        this.contador += 1;

        if(this.contador === 5 ){
          // reject("Error en la promesa");
          resolve("Promesa Exitosa!");
          clearInterval(intervalo);
        }

      }, 1000);

    });

    promesa.then(
      // () => this.state = "Termino!"
      result => this.state = JSON.stringify(result)
    ).catch(error => this.state = error);
  }

  ngOnInit() {

    this.contarTresSegundos().then(result => this.state2 = result )
    .catch(error => console.log(error));

  }

  contarTresSegundos() : Promise<boolean>{
    return new Promise((resolve, reject) => {
      let contador : number = 0;
      let interval = setInterval(() => {
        contador += 1;

        if(contador == 3){
          resolve(true)
          clearInterval(interval);
        }

      }, 1000)

    });
  }

}
