import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ReadService {

  public isReading : boolean;

  constructor() {
    this.isReading = true;
   }
}
