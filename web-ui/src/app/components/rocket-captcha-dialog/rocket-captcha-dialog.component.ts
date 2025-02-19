import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { rocketMoves } from '../../../utils/rocketCaptcha';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-rocket-captcha-dialog',
  imports: [MatDialogModule, MatButtonModule, MatButtonModule],
  templateUrl: './rocket-captcha-dialog.component.html',
  styleUrl: './rocket-captcha-dialog.component.css',
})
export class RocketCaptchaDialogComponent implements OnInit {
  constructor(private http: HttpClient) {}
  rMoves = rocketMoves;
  moves: number[] = [];
  captchaImgUrl = '';
  isLoading = true;
  errorMessage: string | null = null;
  captchaId: string = '';
  readonly dialogRef = inject(MatDialogRef<RocketCaptchaDialogComponent>);

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.isLoading = true;
    this.errorMessage = null;
    this.moves = [];
    this.captchaImgUrl = '';

    this.http
      .get('https://localhost:7153/captcha/rocket', {
        responseType: 'blob',
        observe: 'response',
      })
      .subscribe({
        next: (res) => {
          this.captchaImgUrl = URL.createObjectURL(res.body as Blob);
          this.captchaId = res.headers.get('x-captcha-id') ?? '';
          this.isLoading = false;
        },
        error: (err: HttpErrorResponse) => {
          this.isLoading = false;
        },
      });
  }

  submit() {
    let body = { id: this.captchaId, answer: this.moves };

    this.isLoading = true;
    this.http.post('https://localhost:7153/captcha/rocket', body).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.dialogRef.close();
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.error;
        this.isLoading = false;
      },
    });
  }

  undoLastMove() {
    this.moves.pop();
  }
  storeMove(moveNumber: number) {
    this.moves.push(moveNumber);
  }
  getMoveName(moveNumber: number) {
    switch (moveNumber) {
      case rocketMoves.up:
        return 'Up';

      case rocketMoves.down:
        return 'Down';

      case rocketMoves.left:
        return 'Left';

      case rocketMoves.right:
        return 'Right';

      default:
        return 'Unknown';
    }
  }
}
