import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaygroundLandingPageComponent } from './playground-landing-page.component';

describe('LandingPageComponent', () => {
  let component: PlaygroundLandingPageComponent;
  let fixture: ComponentFixture<PlaygroundLandingPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlaygroundLandingPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaygroundLandingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
