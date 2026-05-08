import { Routes } from '@angular/router';
import { Exam } from './pages/exam/exam';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'exam',
    pathMatch: 'full'
  },
  {
    path: 'exam',
    component: Exam
  }
];