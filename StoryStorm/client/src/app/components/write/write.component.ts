import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {NgForm} from '@angular/forms';
import { WriteService } from '../../services/write.service';
import { Chapter } from 'src/app/classes/chapter';

@Component({
  selector: 'app-write',
  templateUrl: './write.component.html',
  styleUrls: ['./write.component.css']
})
export class WriteComponent implements OnInit {

  contentForm: FormGroup;

  chapter : Chapter;

  constructor(public writeService: WriteService) { }

  ngOnInit() {
    /*
    this.contentForm = new FormGroup({
      Content: new FormControl(null, [Validators.required])
    });*/

    this.writeService.newChapter(this.chapter)
      .subscribe(data => {
        //console.log(data);
      })

  }

  /*
  onSubmit(form: NgForm): void {
    alert(form.value.name);
  }*/

}
