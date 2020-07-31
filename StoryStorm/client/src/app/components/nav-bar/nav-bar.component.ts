import { Component, OnInit } from '@angular/core';
import { User, UserService } from '../../services/user.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(public userService: UserService) { }

  isLoggedIn : boolean;

  ngOnInit() {
    if (localStorage.getItem("userToken") != null){
      this.isLoggedIn = true;
    }
    else{
      this.isLoggedIn = false;
    }
  }

}
