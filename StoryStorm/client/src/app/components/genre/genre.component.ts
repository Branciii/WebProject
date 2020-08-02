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
  allEmployees: Observable<Genre[]>;  

  constructor(private genreService : GenreService) { }

  ngOnInit() {
    this.allEmployees = this.genreService.getAllEmployee();

    /*
    this.genreService.getGenres()
      .subscribe(data => {
        console.log(Object.values(data));
      })*/
  }
}
