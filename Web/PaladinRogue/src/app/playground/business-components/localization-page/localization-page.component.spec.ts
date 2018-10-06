import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LocalizationPageComponent } from './localization-page.component';

describe('LocalizationPageComponent', () => {
  let component: LocalizationPageComponent;
  let fixture: ComponentFixture<LocalizationPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LocalizationPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LocalizationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
