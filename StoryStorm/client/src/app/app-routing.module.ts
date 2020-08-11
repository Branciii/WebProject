import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AboutComponent } from './components/about/about.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { GenreComponent } from './components/genre/genre.component';
import { HomeComponent } from './components/home/home.component';
import { WriteComponent } from './components/write/write.component';
import { ReadComponent } from './components/read/read.component';

const routes: Routes = [
  {path : '', component : HomeComponent},
  {path : 'login', component : LoginComponent},
  {path : 'about', component : AboutComponent},
  {path : 'register', component : RegisterComponent},
  {path : 'genre', component : GenreComponent},
  {path : 'home', component : HomeComponent},
  {path : 'write', component : WriteComponent},
  {path : 'read', component : ReadComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
