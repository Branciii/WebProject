import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';  
import { BehaviorSubject, Observable, throwError } from 'rxjs';  
import { tap, catchError} from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

export type User = { Email? : string, UserName : string, Password : string, ConfirmPassword? : string };

@Injectable({
  providedIn: 'root'
})

export class UserService {

  private url : string = 'https://localhost:44374';  

  public isLoggedIn : boolean;


  constructor(private http: HttpClient, public router: Router , private toastr: ToastrService) { }

  private handleError(error: HttpErrorResponse) {
    /*
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    */
    if (error.error.ModelState[""][0] == undefined){
      console.log(error);
    }
    else{
      this.showToaster(error.error.ModelState[""][0]);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };

  showToaster(message : string){
    this.toastr.error(message);
  }

  register(credentials: User): Observable<any> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    if (credentials.Password == credentials.ConfirmPassword){
      return this.http.post(this.url + '/api/Account/Register',  
      credentials, httpOptions).pipe(
        catchError(this.handleError.bind(this)),
        tap(
          () => {
            this.userAuthentication(credentials).subscribe((data : any)=>{
              localStorage.setItem('userToken',data.access_token);
              this.checkLogged();
              this.router.navigate(['genre']);
            })
          }
        ), 
      );
    }
    else{
      this.showToaster("Passwords don't match");
    }
  }

  login(credentials: User) {
    this.userAuthentication(credentials).subscribe(
      res => {
              localStorage.setItem('userToken',res.access_token);
              //console.log("this is the token received", res.access_token);
              this.checkLogged();
              this.router.navigate(['/home']);}   ,
      err => {
              //console.log('HTTP Error', err)
              this.showToaster("Wrong email or password");
            },
      //() => console.log('HTTP request completed.')
    );
  }

  userAuthentication(credentials: User) : Observable<any> {
    var data = "username=" + credentials.UserName + "&password=" + credentials.Password + "&grant_type=password";
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded','No-Auth':'True' });
    return this.http.post(this.url + '/token', data, { headers: reqHeader });
  }

  checkLogged() : void {
    if (localStorage.getItem('userToken') != null){
      this.isLoggedIn = true;
    }
    else{
      //console.log("user token is null", localStorage.getItem('userToken'));
      this.isLoggedIn = false;
    }
  }
  
  logout(): void {
    localStorage.clear();
    this.router.navigate(["/login"]);
  }

}
