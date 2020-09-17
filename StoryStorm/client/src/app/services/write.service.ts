import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';  
import { BehaviorSubject, Observable, throwError } from 'rxjs';  
import { tap, catchError} from 'rxjs/operators';
import { Router } from '@angular/router';
import { Chapter } from '../classes/chapter';

@Injectable({
  providedIn: 'root'
})
export class WriteService {

  private url : string = 'https://localhost:44374';  
  private chapter : Chapter;

  constructor(private http: HttpClient, public router: Router ) { }

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

  newChapter(storyId : string, name : string, content : string, chapterNumber : number) : Observable<Chapter>{
    this.chapter = {StoryId : storyId, Name : name, Content : content, ChapterNumber : chapterNumber}
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post(this.url + '/api/postNewChapter',  
      this.chapter, httpOptions).pipe(
        catchError(this.handleError.bind(this))
        );
  }
}
