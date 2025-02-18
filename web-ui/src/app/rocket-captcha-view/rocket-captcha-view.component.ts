import { MatFormFieldModule } from '@angular/material/form-field';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatDialog } from '@angular/material/dialog';
import { RocketCaptchaDialogComponent } from '../components/rocket-captcha-dialog/rocket-captcha-dialog.component';

@Component({
  selector: 'app-rocket-captcha-view',
  imports: [MatCardModule, MatButtonModule, MatInputModule, MatFormFieldModule],
  templateUrl: './rocket-captcha-view.component.html',
  styleUrl: './rocket-captcha-view.component.css',
})
export class RocketCaptchaViewComponent {
  constructor(private dialog: MatDialog) {}
  openCaptcha() {
    const dialogRef = this.dialog.open(RocketCaptchaDialogComponent, {
      width: '80vw',
      height: '98vh',
    });
  }
}
