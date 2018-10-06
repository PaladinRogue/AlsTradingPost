import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SummaryFieldComponent } from './summary-field.component';

describe('SummaryFieldComponent', () => {
  let component: SummaryFieldComponent;
  let fixture: ComponentFixture<SummaryFieldComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SummaryFieldComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SummaryFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
