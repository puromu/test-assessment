import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Choice {
  id: number;
  text: string;
}

export interface Question {
  id: number;
  text: string;
  choices: Choice[];
  correctChoiceId: number;
}

export interface SubmitResultRequest {
  fullName: string;
  score: number;
  total: number;
}

@Injectable({
  providedIn: 'root'
})
export class ExamService {

  private readonly baseUrl =
    'http://localhost:5209/api/assessment';

  constructor(
    private http: HttpClient
  ) {}

  getQuestions(): Observable<Question[]> {

    return this.http.get<Question[]>(
      `${this.baseUrl}/questions`
    );
  }

  submitResult(
    data: SubmitResultRequest
  ): Observable<any> {

    return this.http.post(
      `${this.baseUrl}/results`,
      data
    );
  }
}