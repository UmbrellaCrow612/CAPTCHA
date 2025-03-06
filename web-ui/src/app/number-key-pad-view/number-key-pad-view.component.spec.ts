import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NumberKeyPadViewComponent } from './number-key-pad-view.component';

describe('NumberKeyPadViewComponent', () => {
  let component: NumberKeyPadViewComponent;
  let fixture: ComponentFixture<NumberKeyPadViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NumberKeyPadViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NumberKeyPadViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
