import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(public userService: UserService ) { }

  registerForm: FormGroup;

  ngOnInit() {
    localStorage.clear();
    this.registerForm = new FormGroup({
      Email: new FormControl(null, [Validators.required, Validators.email]),
      Password: new FormControl(null, [Validators.required]),
      ConfirmPassword: new FormControl(null, [Validators.required])
    });
  }

  onSubmit() {
    //console.log("on submit");
    let value: any = this.registerForm.value;

    this.userService.register(value)
      .subscribe(data => {
        console.log(data);
      })

  }

}
