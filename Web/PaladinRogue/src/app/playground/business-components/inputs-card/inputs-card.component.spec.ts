import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InputsCardComponent } from './inputs-card.component';

describe('InputsCardComponent', () => {
  let component: InputsCardComponent;
  let fixture: ComponentFixture<InputsCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InputsCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InputsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
