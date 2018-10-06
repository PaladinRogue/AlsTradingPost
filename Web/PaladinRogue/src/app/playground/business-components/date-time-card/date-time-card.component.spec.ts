import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DateTimeCardComponent } from './date-time-card.component';

describe('DateTimePageComponent', () => {
  let component: DateTimeCardComponent;
  let fixture: ComponentFixture<DateTimeCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DateTimeCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DateTimeCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
