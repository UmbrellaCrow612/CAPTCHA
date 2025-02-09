import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TileCaptchaViewComponent } from './tile-captcha-view.component';

describe('TileCaptchaViewComponent', () => {
  let component: TileCaptchaViewComponent;
  let fixture: ComponentFixture<TileCaptchaViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TileCaptchaViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TileCaptchaViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
