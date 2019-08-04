import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StoragePageComponent } from './storage-page.component';

describe('StoragePageComponent', () => {
  let component: StoragePageComponent;
  let fixture: ComponentFixture<StoragePageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StoragePageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StoragePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
