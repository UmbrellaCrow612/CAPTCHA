import { Routes } from '@angular/router';
import { TileCaptchaViewComponent } from './tile-captcha-view/tile-captcha-view.component';
import { TextImgCaptchaViewComponent } from './text-img-captcha-view/text-img-captcha-view.component';

export const routes: Routes = [
  {
    path: 'tile',
    title: 'Tile captcha example',
    component: TileCaptchaViewComponent,
  },
  {
    path: 'text-img',
    title: 'Text img captcha',
    component: TextImgCaptchaViewComponent,
  },
];
