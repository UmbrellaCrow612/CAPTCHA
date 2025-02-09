import { NgOptimizedImage } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-tile-captcha-dialog',
  imports: [MatDialogModule, MatFormFieldModule, MatButtonModule, NgOptimizedImage, MatButtonModule],
  templateUrl: './tile-captcha-dialog.component.html',
  styleUrl: './tile-captcha-dialog.component.css',
})
export class TileCaptchaDialogComponent {}
