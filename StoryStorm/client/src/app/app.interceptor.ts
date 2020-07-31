import { Observable, from } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http';
import { HttpRequest } from '@angular/common/http';
import { HttpHandler } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
 
@Injectable()
export class AppInterceptor implements HttpInterceptor {
  constructor() {}
  
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const observable = from(this.handleAccess(request, next));
    return observable;
  }
 
  private async handleAccess(request: HttpRequest<any>, next: HttpHandler):
      Promise<HttpEvent<any>> {
    const token = localStorage.getItem('userToken');
    let changedRequest = request;

    const headerSettings: {[name: string]: string | string[]; } = {};
 
    for (const key of request.headers.keys()) {
      headerSettings[key] = request.headers.getAll(key);
    }
    if (token) {
      headerSettings['Authorization'] = 'Bearer ' + token;
    }
    headerSettings['Content-Type'] = 'application/json';
    const newHeader = new HttpHeaders(headerSettings);
 
    changedRequest = request.clone({
      headers: newHeader});
    return next.handle(changedRequest).toPromise();
  }
 
}