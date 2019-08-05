import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BlankModalContentComponent } from './blank-modal-content.component';

describe('BlankModalContentComponent', () => {
  let component: BlankModalContentComponent;
  let fixture: ComponentFixture<BlankModalContentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlankModalContentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlankModalContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
