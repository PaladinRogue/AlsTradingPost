import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaPageComponent } from './media-page.component';

describe('MediaPageComponent', () => {
  let component: MediaPageComponent;
  let fixture: ComponentFixture<MediaPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MediaPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MediaPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
