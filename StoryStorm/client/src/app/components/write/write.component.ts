import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-write',
  templateUrl: './write.component.html',
  styleUrls: ['./write.component.css']
})
export class WriteComponent implements OnInit {

  contentForm: FormGroup;

  constructor() { }

  ngOnInit() {
    this.contentForm = new FormGroup({
      Content: new FormControl(null, [Validators.required])
    });
  }

  /*
  onSubmit(form: NgForm): void {
    alert(form.value.name);
  }*/

}
