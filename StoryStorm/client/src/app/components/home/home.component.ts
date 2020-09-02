import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/services/story.service';
import { Observable } from 'rxjs';
import { Story } from '../../classes/story';
import { Genre } from '../../classes/genre';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  recommendedStories: Story[];  

  storyGenres: Observable<Genre[]>;  

  constructor(public storyService: StoryService, public router: Router) { }

  ngOnInit() {

    if(localStorage.getItem("userToken")!=null){
      this.storyService.getStories()
      .subscribe(data => {
        this.recommendedStories = data as Story[];
        console.log(data);
      })
    }
    else{
      //console.log("navigating to about");
      this.router.navigate(['/login']);
    }
    
  }

  showStory(storyId : string){
   // this.storyGenres = this.storyService.getStoryGenres('storyId');
    console.log(storyId);
  }

}
