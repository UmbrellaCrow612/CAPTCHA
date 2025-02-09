import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TileCaptchaDialogComponent } from './tile-captcha-dialog.component';

describe('TileCaptchaDialogComponent', () => {
  let component: TileCaptchaDialogComponent;
  let fixture: ComponentFixture<TileCaptchaDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TileCaptchaDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TileCaptchaDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
