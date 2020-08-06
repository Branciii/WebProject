import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Genre } from '../classes/genre';


@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor( private http: HttpClient, private router: Router ) {  }

  private url : string = 'https://localhost:44374';  


  getGenres(): Observable<Genre[]> {
    if (localStorage.getItem("userToken")==null){
      this.router.navigate(['login']);
    }  
    return this.http.get<Genre[]>(this.url + '/api/getGenres');  
  }  

}
