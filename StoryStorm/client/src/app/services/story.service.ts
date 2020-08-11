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
}