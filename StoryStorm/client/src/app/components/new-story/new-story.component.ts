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

  storyForm: FormGroup;

  allGenres: Observable<Genre[]>;  

  constructor(private storyService: StoryService, private genreService: GenreService) { }

  ngOnInit() {
    this.allGenres = this.genreService.getGenres();
    this.storyForm = new FormGroup({
      Title: new FormControl(null, [Validators.required]),
      Description: new FormControl(null, [Validators.required])
    });
  }

}
