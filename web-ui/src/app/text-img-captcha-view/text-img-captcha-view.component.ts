import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaderResponse,
  HttpResponse,
} from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-text-img-captcha-view',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
  ],
  templateUrl: './text-img-captcha-view.component.html',
  styleUrl: './text-img-captcha-view.component.css',
})
export class TextImgCaptchaViewComponent implements OnInit {
  constructor(private httpClient: HttpClient) {}
  ngOnInit(): void {
    this.fetchData();
  }

  errorMessage: string | null = null;
  isLoading = true;
  imgUrl: string = '';
  captchaId: string = '';
  answer = new FormControl('', [Validators.required]);
  captchaPassed = false;

  fetchData() {
    this.isLoading = true;
    this.errorMessage = null;
    this.captchaPassed = false;
    this.answer.setValue('');
    this.httpClient
      .get('https://localhost:7153/captcha/text-img', {
        responseType: 'blob',
        observe: 'response',
      })
      .subscribe({
        next: (res) => {
          const objectURL = URL.createObjectURL(res.body as Blob);
          this.imgUrl = objectURL;
          this.captchaId = res.headers.get('x-captcha-id') ?? '';
          this.isLoading = false;
        },
        error: (error: HttpErrorResponse) => {
          this.isLoading = false;
        },
      });
  }

  submitCaptcha() {
    let body = { id: this.captchaId, answer: this.answer.getRawValue() };
    this.isLoading = true;

    this.httpClient
      .post('https://localhost:7153/captcha/text-img', body)
      .subscribe({
        next: (res) => {
          this.isLoading = false;
          this.captchaPassed = true;
        },
        error: (error: HttpErrorResponse) => {
          this.errorMessage = error.error.message;
          this.isLoading = false;
        },
      });
  }
}
