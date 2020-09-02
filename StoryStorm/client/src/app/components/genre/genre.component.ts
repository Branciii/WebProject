import { Component, OnInit } from '@angular/core';
import { GenreService } from '../../services/genre.service';
//import { Observable } from 'rxjs';
import { Genre } from '../../classes/genre';
import { Router } from '@angular/router';

@Component({
  selector: 'app-genre',
  templateUrl: './genre.component.html',
  styleUrls: ['./genre.component.css']
})
export class GenreComponent implements OnInit {

  notPickedGenres: Genre[];  

  pickedGenres: Array<Genre>;

  constructor(private genreService : GenreService, private router : Router) { }

  ngOnInit() {
    //this.allGenres = this.genreService.getGenres();
    //console.log(this.allGenres);

    if (localStorage.getItem("userToken")==null){
      this.router.navigate(['login']);
    }  
    else{
      this.genreService.getNotPickedGenres()
        .subscribe(data => {
          this.notPickedGenres = data as Genre[];
          console.log(Object.values(data));
        })

      this.genreService.getPickedGenres()
        .subscribe(data => {
          this.pickedGenres = data as Genre[];
          console.log(Object.values(data));
        })  
    }


  }

  addPickedGenre(genre : Genre){
    this.pickedGenres.push(genre);
    this.notPickedGenres = this.notPickedGenres.filter(function(value){ return value.GenreID != genre.GenreID ;});
  }

  deletePickedGenre(genre : Genre){
    this.pickedGenres = this.pickedGenres.filter(function(value){ return value.GenreID != genre.GenreID ;});
    this.notPickedGenres.push(genre);
  }

  savePickedGenres(){
    this.genreService.savePickedGenres(this.pickedGenres)
      .subscribe(data => {
        //console.log(data);
      })
    this.router.navigate(['home']);
  }

}
