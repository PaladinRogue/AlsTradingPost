import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IconActionComponent } from './icon-action.component';

describe('ActionComponent', () => {
  let component: IconActionComponent;
  let fixture: ComponentFixture<IconActionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IconActionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IconActionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
