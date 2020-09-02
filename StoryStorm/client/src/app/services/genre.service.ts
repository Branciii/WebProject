import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { tap, catchError} from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { Genre } from '../classes/genre';


@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor( private http: HttpClient ) {  }

  private url : string = 'https://localhost:44374';  


  getGenres(): Observable<Genre[]> {
    return this.http.get<Genre[]>(this.url + '/api/getGenres');  
  } 

  getNotPickedGenres(): Observable<Genre[]> {
    return this.http.get<Genre[]>(this.url + '/api/getOtherGenres');  
  } 

  getPickedGenres(): Observable<Genre[]> {
    return this.http.get<Genre[]>(this.url + '/api/getUsersGenres');  
  } 
  
  savePickedGenres(pickedGenres : Array<Genre>){
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post(this.url + '/api/postUserGenres', pickedGenres, httpOptions).pipe(
        catchError(this.handleError.bind(this))
        );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };

}
