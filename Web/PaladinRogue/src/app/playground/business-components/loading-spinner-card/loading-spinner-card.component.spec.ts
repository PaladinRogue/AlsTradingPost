import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadingSpinnerCardComponent } from './loading-spinner-card.component';

describe('LoadingSpinnerCardComponent', () => {
  let component: LoadingSpinnerCardComponent;
  let fixture: ComponentFixture<LoadingSpinnerCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoadingSpinnerCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoadingSpinnerCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
