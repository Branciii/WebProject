import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';  
import { BehaviorSubject, Observable, throwError } from 'rxjs';  
import { tap, catchError} from 'rxjs/operators';
import { Router } from '@angular/router';
import { Chapter } from '../classes/chapter';

@Injectable({
  providedIn: 'root'
})
export class ReadService {

  private url : string = 'https://localhost:44374';  

  public isReading : boolean;

  constructor(private http: HttpClient, public router: Router ) {
    this.isReading = true;
   }

   /*
   getChapter(storyId : string, chapterNumber : number) : Observable<any>{
    let params = new HttpParams().set("StoryId",storyId).set("ChapterNumber",chapterNumber.toString());
    return this.http.get<Chapter>(this.url + '/api/getChapter',{params: params});  
  }*/

  getChapter(storyId : string) : Observable<any>{
    let params = new HttpParams().set("StoryId",storyId);
    return this.http.get<string>(this.url + '/api/getChapter',{params: params});  
  }

  getAnotherChapter(storyId : string, chapterNumber : number) : Observable<any> {
    console.log("received",chapterNumber);
    let params = new HttpParams().set("StoryId",storyId).set("ChapterNumber",chapterNumber.toString());
    return this.http.get<string>(this.url + '/api/getChapterByNumber',{params: params});  
  }

  isLastChapter(storyId : string, chapterNumber : number) : Observable<any> {
    let params = new HttpParams().set("StoryId",storyId).set("ChapterNumber",chapterNumber.toString());
    return this.http.get<string>(this.url + '/api/isItLastChapter',{params: params});  
  }
}
