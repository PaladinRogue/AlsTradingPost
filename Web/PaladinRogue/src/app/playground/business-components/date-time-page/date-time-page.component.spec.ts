import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DateTimePageComponent } from './date-time-page.component';

describe('DateTimePageComponent', () => {
  let component: DateTimePageComponent;
  let fixture: ComponentFixture<DateTimePageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DateTimePageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DateTimePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
