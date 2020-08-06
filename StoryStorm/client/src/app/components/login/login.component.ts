import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public userService: UserService) { }

  loginForm: FormGroup;

  ngOnInit() {
    localStorage.clear();
    this.loginForm = new FormGroup({
      UserName: new FormControl(null, [Validators.required]),
      Password: new FormControl(null, [Validators.required])
    });
  }

  onSubmit() {
    let value: any = this.loginForm.value;
    this.userService.login(value);
  }
}
