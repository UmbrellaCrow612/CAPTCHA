import { HomeViewComponent } from './home-view/home-view.component';
import { Routes } from '@angular/router';
import { TileCaptchaViewComponent } from './tile-captcha-view/tile-captcha-view.component';
import { TextImgCaptchaViewComponent } from './text-img-captcha-view/text-img-captcha-view.component';
import { AudioCaptchaViewComponent } from './audio-captcha-view/audio-captcha-view.component';

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
  {
    path: 'audio',
    title: 'Audio captcha',
    component: AudioCaptchaViewComponent,
  },
  {
    path: '',
    component: HomeViewComponent,
  },
];
