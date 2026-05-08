import { Component, OnInit,ChangeDetectorRef  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import {
  ExamService,
  Question,
  SubmitResultRequest
} from '../../service/exam';

@Component({
  selector: 'app-exam',
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './exam.html',
  styleUrl: './exam.css'
})
export class Exam implements OnInit {

  fullName = '';
  nameTouched = false;
  score = 0;
  isSubmitted = false;
  isLoading = false;
  errorMessage = '';

  questions: Question[] = [];

  answers: { [questionId: number]: number } = {};

  constructor(
  private examService: ExamService,
  private cdr: ChangeDetectorRef
) {}

  ngOnInit(): void {
    this.loadQuestions();
  }

  loadQuestions(): void {
  this.isLoading = true;
  this.errorMessage = '';

  this.examService.getQuestions().subscribe({
    next: (res) => {
      console.log('questions from api:', res);
      console.log('questions length:', res.length);

      this.questions = res;
      this.isLoading = false;
      this.cdr.detectChanges();
    },
    error: (err) => {
      console.error('load questions error:', err);
      console.error('status:', err.status);
      console.error('url:', err.url);

      this.errorMessage = 'โหลดข้อสอบไม่สำเร็จ';
      this.isLoading = false;
      
      this.cdr.detectChanges();
    }
  });
}

  submitExam(): void {
    this.nameTouched = true;

    if (!this.fullName.trim()) {
      this.errorMessage = 'กรุณากรอกชื่อ';
      return;
    }

    if (!this.isAllAnswered()) {
      this.errorMessage = 'กรุณาตอบคำถามให้ครบ';
      return;
    }

  this.errorMessage = '';
  this.score = 0;

  for (const question of this.questions) {
    if (this.answers[question.id] === question.correctChoiceId) {
      this.score++;
    }
  }

  this.isSubmitted = true;

  const payload: SubmitResultRequest = {
    fullName: this.fullName,
    score: this.score,
    total: this.questions.length
  };

  this.examService.submitResult(payload).subscribe({
    next: () => {
      console.log('ส่งคะแนนสำเร็จ');
    },
    error: (err) => {
      console.error('ส่งคะแนนไม่สำเร็จ', err);
      this.errorMessage = 'แสดงคะแนนแล้ว แต่บันทึกผลไม่สำเร็จ';
    }
  });
}

  resetExam(): void {
    this.fullName = '';
    this.nameTouched = false;
    this.score = 0;
    this.answers = {};
    this.isSubmitted = false;
    this.errorMessage = '';
  }

  isAllAnswered(): boolean {
    return this.questions.every(
      q => this.answers[q.id] !== undefined
    );
  }

  isFullNameInvalid(): boolean {
  return !this.fullName || this.fullName.trim().length === 0;
  }
}