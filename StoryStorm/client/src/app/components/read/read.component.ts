import { Component, OnInit } from '@angular/core';
import { ReadService } from '../../services/read.service';
import { Chapter } from '../../classes/chapter';


@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
  styleUrls: ['./read.component.css']
})
export class ReadComponent implements OnInit {

  constructor(public readService: ReadService) { }

  chapter : Chapter;

  ngOnInit() {
    this.chapter = {StoryId:"",Name:"",ChapterNumber:0,Content:""};
    this.readService.getChapter("EC949E04-801B-41F7-B161-ED243FC83880",4)
      .subscribe(data => {
        this.chapter = {StoryId : data.StoryId, Name : data.Name, ChapterNumber:data.ChapterNumber, Content : data.Content};
        console.log(this.chapter.Content);
      })
  }

}
