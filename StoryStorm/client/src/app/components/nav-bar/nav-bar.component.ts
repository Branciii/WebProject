import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { ReadService } from '../../services/read.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(public userService: UserService, public readService: ReadService) { }

  ngOnInit() {
    this.userService.checkLogged();
    console.log("logged in",this.userService.isLoggedIn);
  }
}
