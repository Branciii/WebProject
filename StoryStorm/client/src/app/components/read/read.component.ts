import { Component, OnInit } from '@angular/core';
import { ReadService } from '../../services/read.service';
import { Chapter } from '../../classes/chapter';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
  styleUrls: ['./read.component.css']
})
export class ReadComponent implements OnInit {

  constructor(private route: ActivatedRoute, public readService: ReadService) { }

  chapter : Chapter;
  last : boolean;
  StoryId : string;

  ngOnInit() {
    this.StoryId = this.route.snapshot.params['StoryId'];
    console.log(this.StoryId);
    this.chapter = {StoryId:"",Name:"",ChapterNumber:0,Content:""};
    this.readService.getChapter(this.StoryId)
      .subscribe(data => {
        console.log(data);
        this.chapter = {StoryId : data.StoryId, Name : data.Name, ChapterNumber:data.ChapterNumber, Content : data.Content};
        this.isLastChapter(this.chapter.ChapterNumber);
        console.log(this.chapter.Content);
      })
  }

  otherChapter(chapterNumber : number){
    this.readService.getAnotherChapter(this.StoryId, chapterNumber)
    .subscribe(data => {
      console.log(data);
      this.chapter = {StoryId : data.StoryId, Name : data.Name, ChapterNumber:data.ChapterNumber, Content : data.Content};
      console.log(this.chapter.Content);
    })
    this.isLastChapter(chapterNumber);
  }

  isLastChapter(chapterNumber : number){
    this.readService.isLastChapter(this.StoryId, chapterNumber)
    .subscribe(data => {
      console.log('is last', data, 'for chapter', chapterNumber);
      this.last = data;
    })
  }
}
