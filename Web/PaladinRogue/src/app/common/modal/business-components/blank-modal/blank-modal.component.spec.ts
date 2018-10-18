import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlankModalComponent } from './blank-modal.component';

describe('DefaultModalComponent', () => {
  let component: BlankModalComponent;
  let fixture: ComponentFixture<BlankModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlankModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlankModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
