import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { TileCaptchaDialogComponent } from '../components/tile-captcha-dialog/tile-captcha-dialog.component';

@Component({
  selector: 'app-tile-captcha-view',
  imports: [MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule],
  templateUrl: './tile-captcha-view.component.html',
  styleUrl: './tile-captcha-view.component.css',
})
export class TileCaptchaViewComponent {
  captchaVerified = false;

  constructor(private dialog: MatDialog) {}

  verifyCaptchaClicked() {
    this.dialog.open(TileCaptchaDialogComponent);
  }
}
