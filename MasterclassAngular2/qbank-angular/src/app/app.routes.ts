import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { KlantenComponent } from './components/klanten/klanten.component';

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'klanten', component: KlantenComponent},
];
