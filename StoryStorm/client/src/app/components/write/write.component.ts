import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {NgForm} from '@angular/forms';
import { WriteService } from '../../services/write.service';
import { Chapter } from 'src/app/classes/chapter';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-write',
  templateUrl: './write.component.html',
  styleUrls: ['./write.component.css']
})
export class WriteComponent implements OnInit {

  contentForm: FormGroup;

  chapter : Chapter;
  chapterForm: FormGroup;
  StoryId : string;

  constructor(public router : Router, private route: ActivatedRoute, public writeService: WriteService) { }

  ngOnInit() {
    this.chapterForm = new FormGroup({
      Name: new FormControl(null, [Validators.required]),
      Content: new FormControl(null, [Validators.required])
    });
    this.StoryId = this.route.snapshot.params['StoryId'];
    console.log(this.StoryId);
  }

  onSubmit(form: NgForm): void {
    let value: any = this.chapterForm.value;
    this.writeService.newChapter(this.StoryId, value.Name, value.Content, 1)
      .subscribe(data => {
      })
    this.router.navigate(['home']);
  }
/*
  nextChapter(){
    this.router.navigate(['/write', this.chapter.StoryId]);
  }*/
}
