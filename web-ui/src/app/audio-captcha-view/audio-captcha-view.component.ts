import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-audio-captcha-view',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
  ],
  templateUrl: './audio-captcha-view.component.html',
  styleUrl: './audio-captcha-view.component.css',
})
export class AudioCaptchaViewComponent implements OnInit {
  constructor(private http: HttpClient) {}

  errorMessage: null | string = null;
  captchaPassed = false;
  audioUrl = '';
  isLoading = false;
  answerInput = new FormControl('', [Validators.required]);
  captchaId = '';

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.isLoading = true;
    this.errorMessage = null;
    this.captchaId = '';
    this.answerInput.reset();

    this.http
      .get('https://localhost:7153/captcha/audio', {
        responseType: 'blob',
        observe: 'response',
      })
      .subscribe({
        next: (res) => {
          this.audioUrl = URL.createObjectURL(res.body as Blob);
          this.captchaId = res.headers.get('x-captcha-id') ?? '';
          this.isLoading = false;
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.error.message;
          this.isLoading = false;
        },
      });
  }

  submitData() {
    this.isLoading = true;
    let body = { id: this.captchaId, answer: this.answerInput.getRawValue() };

    this.http.post('https://localhost:7153/captcha/audio', body).subscribe({
      next: (res) => {
        this.captchaPassed = true;
        this.isLoading = false;
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.error.message;
        this.isLoading = false;
      },
    });
  }
}
