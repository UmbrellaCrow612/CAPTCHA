import { TileCAPTCHAPassedService } from './../services/tile-captchapassed.service';
import { Component, OnInit } from '@angular/core';
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
export class TileCaptchaViewComponent implements OnInit {
  captchaPassed = false;

  constructor(
    private dialog: MatDialog,
    private sharedState: TileCAPTCHAPassedService
  ) {}
  ngOnInit(): void {
    this.sharedState.currentState.subscribe((value) => {
      this.captchaPassed = value;
    });
  }

  verifyCaptchaClicked() {
    this.dialog.open(TileCaptchaDialogComponent, {
      width: '76vw',
      maxWidth: '76vw',
      height: '75vh',
    });
  }
}
