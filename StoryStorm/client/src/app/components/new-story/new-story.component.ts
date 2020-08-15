import { Component, OnInit } from '@angular/core';
import { StoryService } from '../../services/story.service';
import { GenreService } from '../../services/genre.service';
import { Observable } from 'rxjs';
import { Genre } from '../../classes/genre';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-new-story',
  templateUrl: './new-story.component.html',
  styleUrls: ['./new-story.component.css']
})
export class NewStoryComponent implements OnInit {

  selectedOption:any;

  storyForm: FormGroup;

  allGenres: Observable<Genre[]>;  

  pickedGenres : Array<Genre>;

  constructor(private storyService: StoryService, private genreService: GenreService) { }

  ngOnInit() {
    this.pickedGenres = [];
    this.selectedOption = 0;
    this.allGenres = this.genreService.getGenres();
    this.storyForm = new FormGroup({
      Title: new FormControl(null, [Validators.required]),
      Description: new FormControl(null, [Validators.required])
    });
  }

  addGenre(genre : Genre){
    const found = this.pickedGenres.some(el => el.GenreID === genre.GenreID);
    if(!found){
      this.pickedGenres.push(genre);
    }
    //console.log(this.pickedGenres);
  }

  deleteGenre(genreId : string){
    this.pickedGenres = this.pickedGenres.filter(function(value){ return value.GenreID != genreId ;});
  }

  onSubmit() {
    let value: any = this.storyForm.value;
    this.storyService.newStory(value.Title, value.Description, this.pickedGenres)
      .subscribe(data => {
        console.log(data);
      })
  }
}
