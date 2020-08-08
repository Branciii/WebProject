import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';  
import { BehaviorSubject, Observable, throwError } from 'rxjs';  
import { tap, catchError} from 'rxjs/operators';
import { Router } from '@angular/router';
import { Story } from '../classes/story';

@Injectable({
  providedIn: 'root'
})
export class StoryService {

  private url : string = 'https://localhost:44374';  

  public user: BehaviorSubject<Story> = new BehaviorSubject<Story>(null);

  constructor(private http: HttpClient, public router: Router) { }

  getStories(): Observable<Story[]> {
    if (localStorage.getItem("userToken")==null){
      this.router.navigate(['login']);
    }  
    return this.http.get<Story[]>(this.url + '/api/getStories');  
  }  
}