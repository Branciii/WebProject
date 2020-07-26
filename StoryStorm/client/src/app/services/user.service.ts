import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';  
import { BehaviorSubject, Observable, throwError } from 'rxjs';  
import { tap, catchError, retry } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import {Token} from '../classes/token';

export type User = { Username: string, Email: string, UserID: any };
export type Credentials = { Username: string, Email: string, Password: string };

@Injectable({
  providedIn: 'root'
})

export class UserService {

  private url : string = 'https://localhost:44336/api';  
  public User: BehaviorSubject<User> = new BehaviorSubject<User>(null);

  constructor(private http: HttpClient, public router: Router , private toastr: ToastrService) { 
    if (this.isLoggedIn())
      this.User.next(this.credentials());
  }

  isLoggedIn() {
    let token = Token.get();
    if (token) {
      let payload = Token.parse(token);
      return payload.exp > Date.now() / 1000;
    } else return false;
  }

  credentials(): User {
    if (this.isLoggedIn()) {
      let token = Token.get();
      let payload = Token.parse(token);
      delete payload["iat"];
      delete payload["exp"];
      return payload;
    } return null;
  }

  saveToken(token: string) {
    Token.save(token);
    this.User.next(this.credentials());
  }

  private handleError(error: HttpErrorResponse) {
    /*
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }*/

    if(error.status == 400 || error.status == 412){
      console.error('bad request');
      this.showToaster();
    }

    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };

  showToaster(){
    this.toastr.error("Sorry, Email or Username are taken")
  }

  register(credentials: Credentials): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post(this.url + '/newUser',  
    credentials, httpOptions).pipe(tap(
      () => {
        this.router.navigate(['login']);
      }
    ), catchError(this.handleError.bind(this)));
    /*
    return this.http.post(this.url, credentials)
      .pipe(
        tap(
          (response: { message: string, token: string }) => {
            this.saveToken(response.token);
            this.router.navigate(['files']);
          }
        ),
        catchError(this.handleError)
      );*/
  }

  /*
  login(credentials: Credentials): Observable<any> {
    return this.http.post(this.url + "/login", credentials)
      .pipe(
        tap(
          (response: { message: string, token?: string }) => {
            if (response["token"]) {
              this.saveToken(response.token);
              this.router.navigate(['files']);
            }
          }
        ),
        catchError(this.handleError)
      );
  }
*/
  /*
  logout(): void {
    Token.remove();
    this.router.navigate(["/login"]);
    this.user.next(null);
  }*/

  authHeaders() {
    return new HttpHeaders({
      'Authorization': `Bearer ${Token.get() ? Token.get() : ""}`
    });
  }
}
