import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
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
  data: captchaData = { id: '', imgUrl: '', matrix: [] };
  constructor(private httpClient: HttpClient) {}
  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.isLoading = true;
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
}

interface captchaData {
  imgUrl: string;
  matrix: number[][];
  id: string;
}
