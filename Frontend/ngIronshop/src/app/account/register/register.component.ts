import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ValidatorReactive } from '../../commons/validators/validators.reactive';
import swal from 'sweetalert';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;

  constructor() { }

  /*@FM:Validation at Form Level! Not a Field Level
   ValidatorReactive.areEquals is a special Form Level validation  
  */
  ngOnInit() {
    this.registerForm = new FormGroup(
      {
        fullName : new FormControl(null, Validators.required),
        email: new FormControl(null, [Validators.required, Validators.email]),
        password: new FormControl(null, Validators.required),
        confirmPassword: new FormControl(null, Validators.required),
        acceptConditions: new FormControl(false)
      },
      { validators: ValidatorReactive.areEquals('password','confirmPassword')}
    );
  }

  onConfirmRegister(){

    if( this.registerForm.invalid){
      return;
    }

    if( !this.registerForm.value.acceptConditions ){
      swal('Attention', 'You must accept conditions', 'warning');
      return;
    }
    


    console.log(this.registerForm.value);
  }

}
