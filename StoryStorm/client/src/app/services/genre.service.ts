import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { tap, catchError} from 'rxjs/operators';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor( private http: HttpClient, private router: Router ) {  }

  private url : string = 'https://localhost:44374';  

  getGenres(): Observable<any>  {
    if (localStorage.getItem("userToken")==null){
      this.router.navigate(['login']);
    }
    return this.http.get(this.url + '/api/getGenres');
  }
}
