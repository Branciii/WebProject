import { Component, OnInit } from '@angular/core';
import { GenreService } from '../../services/genre.service';
import { Observable } from 'rxjs';
import { Genre } from '../../classes/genre';

@Component({
  selector: 'app-genre',
  templateUrl: './genre.component.html',
  styleUrls: ['./genre.component.css']
})
export class GenreComponent implements OnInit {

  allGenres: Observable<Genre[]>;  

  isClicked : Array<boolean>;

  GenreIDs: Array<string>;

  constructor(private genreService : GenreService) { }

  ngOnInit() {
    this.allGenres = this.genreService.getGenres();
    this.GenreIDs = [];
    this.isClicked = [];
    console.log(this.allGenres);

    /*
    this.genreService.getGenres()
      .subscribe(data => {
        this.allGenres = data as Genre[];
        console.log(Object.values(data));
      })*/
  }

  addGenre(id:string){
    this.GenreIDs.push(id);
    console.log(this.GenreIDs);
  }
}
