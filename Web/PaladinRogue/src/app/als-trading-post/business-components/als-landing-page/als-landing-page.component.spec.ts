import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlsLandingPageComponent } from './als-landing-page.component';

describe('LandingPageComponent', () => {
  let component: AlsLandingPageComponent;
  let fixture: ComponentFixture<AlsLandingPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlsLandingPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlsLandingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
