import { HomeViewComponent } from './home-view/home-view.component';
import { Routes } from '@angular/router';
import { TileCaptchaViewComponent } from './tile-captcha-view/tile-captcha-view.component';
import { TextImgCaptchaViewComponent } from './text-img-captcha-view/text-img-captcha-view.component';
import { AudioCaptchaViewComponent } from './audio-captcha-view/audio-captcha-view.component';
import { RocketCaptchaViewComponent } from './rocket-captcha-view/rocket-captcha-view.component';
import { NumberKeyPadViewComponent } from './number-key-pad-view/number-key-pad-view.component';

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
    path: 'rocket',
    title: 'Rocket captcha',
    component: RocketCaptchaViewComponent,
  },
  {
    path: 'number-pad',
    component: NumberKeyPadViewComponent,
  },
  {
    path: '',
    component: HomeViewComponent,
  },
];
