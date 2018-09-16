import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IconBadgeComponent } from './icon-badge.component';

describe('IconBadgeComponent', () => {
  let component: IconBadgeComponent;
  let fixture: ComponentFixture<IconBadgeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IconBadgeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IconBadgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
