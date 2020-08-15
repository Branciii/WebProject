import { Component, OnInit } from '@angular/core';
import { StoryService } from '../../services/story.service';
import { GenreService } from '../../services/genre.service';
import { Observable } from 'rxjs';
import { Genre } from '../../classes/genre';

@Component({
  selector: 'app-new-story',
  templateUrl: './new-story.component.html',
  styleUrls: ['./new-story.component.css']
})
export class NewStoryComponent implements OnInit {

  allGenres: Observable<Genre[]>;  

  constructor(private storyService: StoryService, private genreService: GenreService) { }

  ngOnInit() {
    this.allGenres = this.genreService.getGenres();
  }

}
