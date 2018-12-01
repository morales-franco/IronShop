import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscriber, Subscription } from 'rxjs';
import { retry, map, filter } from 'rxjs/operators';

@Component({
  selector: 'app-rxjs',
  templateUrl: './rxjs.component.html',
  styles: []
})
export class RxjsComponent implements OnInit, OnDestroy {

/*
@FM:
//Generamos un observador

let obs = new Observable( observer => {
  //Devuelve información a sus subscribers
  observer.next(contador);

  //Finaliza y avisa a sus subscribers
  observer.complete();

  //Se produce un error, finaliza y avisa del error a los subscribers
   observer.error(dataError)

});

//Nos suscribimos al Obsevable
obs.subscribe(
      result => console.log("Return: ", result ),
      error => console.error(error),
      () => console.log("El observador termino!")
);

//Retry: En caso que el observable de error podemos especificar que reintente una cantidad finita o infinita de veces

En este caso Intenta 2 veces.
 obs
.pipe(retry(2))
.subscribe(
  ...
  );
Ejecuciones:
Ejecución Normal:  --> Dio error
Intento1: --> Dio Error
Intento2 --> Dio Error --> Recién aca notifica el error.

------
En este caso intenta infinitamente:
 obs
.pipe(retry())
.subscribe(
  ...
  );

//Map: aplica una función a cada resultado del observable, permite modificar la información de salida
//Ejemplo de un servidor se recibe un json:
let contadorJson =  {
  valor : contador
}

Yo lo que quiero en realidad es el valor, no el json. Entonces lo mappeo antes de devolverle la data a los
que estan subscriptos:
//(Esto estaria en un service)

new Observable( (observer : Subscriber<any>) => {
  //Comunicación con el servidor, proceso asyn, etc. 
 })
    .pipe( 
      retry(2),
      // map( jsonResponse => { return jsonResponse.valor; } Equivalente sin expresión lambda
      map(jsonResponse => jsonResponse.valor)
      );
Sin el map devuelve: Return:  {valor: 1}
Con el map devuelve: Return:  1
-----------

//Filter: filtra los valores devueltos, si o si debe retornar true o false
Recibe como parametro una función que tiene como parametro el valor y el index, retornando un booleano

filter( (value, index) => {
  return VALOR_BOOLEANO
});

-----------
Al rxjs operators:
http://reactivex.io/documentation/operators.html

-----------
Si tengo un observable que no finaliza nunca y yo lo quiero finalizar, para esto tengo que desusbcribirme.
Lo puedo hacer en el ngOnDestroy.
Esto se hace cuando el observable no ejecute el observer.complete(); por lo tanto sigue emitiendo información eternamente
next().

OnDestroy:
this.subscription.unsubscribe();

Previamente: asigno el resultado de la función que me devuelve un observable a:
subscription : Subscription;


*/

subscription : Subscription;

  constructor() { 

  }

  ngOnInit() {
    this.subscription = this.getObservable()
    .subscribe(
      result => console.log("Return: ", result ),
      error => console.error(error),
      () => console.log("El observador termino!")
    );
  }

  ngOnDestroy(){
    console.log("Se esta saliendo del componente!");
    this.subscription.unsubscribe();
  }

  getObservable() : Observable<any>{
    return new Observable( (observer : Subscriber<any>) => {

      let contador = 0;
      let intervalo = setInterval(() => {
        contador +=1;

        let contadorJson =  {
          valor : contador
        }

        observer.next(contadorJson);

        if(contador === 3 ){
          clearInterval(intervalo);
          observer.complete();
        }

        // if(contador == 2){
        //   clearInterval(intervalo);
        //   observer.error("Upss Algo salio mal!");
        // }

      },1000);

    })
    .pipe( 
      retry(2),
      map(jsonResponse => jsonResponse.valor),
      filter( (value, index) => {
        //Mostramos solo impares

        if((value % 2 ) == 1){
          return true;
        }else{
          return false;
        }

      })
      );
  }

}
