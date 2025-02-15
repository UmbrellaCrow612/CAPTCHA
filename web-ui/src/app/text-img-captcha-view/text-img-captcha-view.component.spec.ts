import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextImgCaptchaViewComponent } from './text-img-captcha-view.component';

describe('TextImgCaptchaViewComponent', () => {
  let component: TextImgCaptchaViewComponent;
  let fixture: ComponentFixture<TextImgCaptchaViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TextImgCaptchaViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TextImgCaptchaViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
