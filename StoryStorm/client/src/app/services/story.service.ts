import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';  
import { BehaviorSubject, Observable, throwError } from 'rxjs';  
import { tap, catchError} from 'rxjs/operators';
import { Router } from '@angular/router';
import { Story } from '../classes/story';
import { Genre } from '../classes/genre';

@Injectable({
  providedIn: 'root'
})
export class StoryService {

  private url : string = 'https://localhost:44374'; 
  
  private story : Story;

  constructor(private http: HttpClient, public router: Router) { }

  getStories(): Observable<Story[]> {
    if (localStorage.getItem("userToken")==null){
      this.router.navigate(['login']);
    }  
    return this.http.get<Story[]>(this.url + '/api/getStories');  
  }  

  getStoryGenres(storyId:string): Observable<Genre[]>{
    let params = new HttpParams().set("StoryId",storyId);
    return this.http.get<Genre[]>(this.url + '/api/getStoryGenres',{params: params});  
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

  newStory(title : string, description : string, pickedGenres : Array<Genre>){
    this.story = {StoryID:"", Title:title, Description:description, Grade:null, Finished:null, Author:null, Genres:pickedGenres};
    console.log(this.story.Genres);
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post(this.url + '/api/postNewStory', this.story, httpOptions).pipe(
        catchError(this.handleError.bind(this))
        );
  }
}