import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormInputAbstract } from './form-input.component';

describe('FormInputComponent', () => {
  let component: FormInputAbstract;
  let fixture: ComponentFixture<FormInputAbstract>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormInputAbstract ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormInputAbstract);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
