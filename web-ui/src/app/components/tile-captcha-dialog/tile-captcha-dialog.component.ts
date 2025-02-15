import { TileCAPTCHAPassedService } from './../../services/tile-captchapassed.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-tile-captcha-dialog',
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    MatButtonModule,
    MatButtonModule,
  ],
  templateUrl: './tile-captcha-dialog.component.html',
  styleUrl: './tile-captcha-dialog.component.css',
})
export class TileCaptchaDialogComponent implements OnInit {
  isLoading = true;
  isSuc = false;
  errorMessage: null | string = null;
  data: captchaData = { id: '', imgUrl: '', matrix: [] };
  constructor(
    private httpClient: HttpClient,
    private sharedState: TileCAPTCHAPassedService,
    private dialogRef: MatDialogRef<TileCaptchaDialogComponent>
  ) {}
  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.isLoading = true;
    this.errorMessage = null;
    this.httpClient
      .get('https://localhost:7153/captcha/tile', {
        responseType: 'blob',
        observe: 'response',
      })
      .subscribe((res) => {
        const imageUrl = URL.createObjectURL(res.body as Blob);
        const matrix = JSON.parse(res.headers.get('X-Base-Matrix') ?? '[]');
        const id = res.headers.get('X-Captcha-Id') ?? 'Null';

        this.data.imgUrl = imageUrl;
        this.data.matrix = matrix;
        this.data.id = id;
        this.isLoading = false;
      });
  }

  handleBtnClick(rowIndex: number, colIndex: number) {
    let currentVal = this.data.matrix[rowIndex][colIndex];
    if (currentVal.toString() === '0') {
      this.data.matrix[rowIndex][colIndex] = 1;
    } else {
      this.data.matrix[rowIndex][colIndex] = 0;
    }
  }

  handleSubmit() {
    this.isLoading = true;
    let body = { id: this.data.id, answer: JSON.stringify(this.data.matrix) };
    this.httpClient
      .post('https://localhost:7153/captcha/tile', body)
      .subscribe({
        next: (res) => {
          this.isLoading = false;
          this.isSuc = true;
          this.errorMessage = null;
          this.sharedState.updateState(true);
          this.dialogRef.close();
        },
        error: (error: HttpErrorResponse) => {
          this.isLoading = false;
          this.isSuc = false;
          this.errorMessage = error.error;
        },
      });
  }
}

interface captchaData {
  imgUrl: string;
  matrix: number[][];
  id: string;
}
