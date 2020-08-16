import { Component, OnInit } from '@angular/core';
import { GenreService } from '../../services/genre.service';
//import { Observable } from 'rxjs';
import { Genre } from '../../classes/genre';

@Component({
  selector: 'app-genre',
  templateUrl: './genre.component.html',
  styleUrls: ['./genre.component.css']
})
export class GenreComponent implements OnInit {

  allGenres: Genre[];  

  pickedGenres: Array<Genre>;

  constructor(private genreService : GenreService) { }

  ngOnInit() {
    //this.allGenres = this.genreService.getGenres();
    //console.log(this.allGenres);

    this.pickedGenres = [];

    this.genreService.getGenres()
      .subscribe(data => {
        this.allGenres = data as Genre[];
        console.log(Object.values(data));
      })
  }

  addPickedGenre(genre : Genre){
    this.pickedGenres.push(genre);
    this.allGenres = this.allGenres.filter(function(value){ return value.GenreID != genre.GenreID ;});
  }

  deletePickedGenre(genre : Genre){
    this.pickedGenres = this.pickedGenres.filter(function(value){ return value.GenreID != genre.GenreID ;});
    this.allGenres.push(genre);
  }

  savePickedGenres(){
    this.genreService.savePickedGenres(this.pickedGenres)
      .subscribe(data => {
        //console.log(data);
      })
  }

}
