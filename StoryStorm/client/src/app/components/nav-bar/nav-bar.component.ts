import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { ReadService } from '../../services/read.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private router : Router, public userService: UserService, public readService: ReadService) { }

  ngOnInit() {
    this.userService.checkLogged();
    console.log("logged in",this.userService.isLoggedIn);
  }
}
