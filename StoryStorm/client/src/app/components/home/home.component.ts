import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/services/story.service';
import { GenreService } from '../../services/genre.service';
import { Observable } from 'rxjs';
import { Story } from '../../classes/story';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  recommendedStories: Observable<Story[]>;  

  constructor(public storyService: StoryService) { }

  ngOnInit() {
    
    this.recommendedStories = this.storyService.getStories();
    this.storyService.getStories()
      .subscribe(data => {
        console.log(data);
      })
  }
}
