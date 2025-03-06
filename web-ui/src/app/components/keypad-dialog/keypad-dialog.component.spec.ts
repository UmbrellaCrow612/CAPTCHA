import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KeypadDialogComponent } from './keypad-dialog.component';

describe('KeypadDialogComponent', () => {
  let component: KeypadDialogComponent;
  let fixture: ComponentFixture<KeypadDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KeypadDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KeypadDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
