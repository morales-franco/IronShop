import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ValidatorReactive } from "../../commons/validators/validators.reactive";
import swal from "sweetalert";
import { UserService } from "../../services/user/user.service";
import { Register } from "../../models/register.model";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(public userService: UserService) {}

  /*@FM:Validation at Form Level! Not a Field Level
   ValidatorReactive.areEquals is a special Form Level validation  
  */
  ngOnInit() {
    this.registerForm = new FormGroup(
      {
        fullName: new FormControl(null, Validators.required),
        email: new FormControl(null, [Validators.required, Validators.email]),
        password: new FormControl(null, Validators.required),
        confirmPassword: new FormControl(null, Validators.required),
        acceptConditions: new FormControl(false)
      },
      { validators: ValidatorReactive.areEquals("password", "confirmPassword") }
    );
  }

  onConfirmRegister() {
    if (this.registerForm.invalid) {
      return;
    }

    if (!this.registerForm.value.acceptConditions) {
      swal("Attention", "You must accept conditions", "warning");
      return;
    }

    let registerUser: Register = new Register(
      this.registerForm.value.fullName,
      this.registerForm.value.email,
      this.registerForm.value.password
    );

    this.userService.register(registerUser)
        .subscribe(u => {
          console.log(u);
        });

    console.log(this.registerForm.value);
  }
}
