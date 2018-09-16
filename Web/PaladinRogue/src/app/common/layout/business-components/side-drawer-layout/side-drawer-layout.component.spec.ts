import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SideDrawerLayoutComponent } from './side-drawer-layout.component';

describe('SideDrawerLayoutComponent', () => {
  let component: SideDrawerLayoutComponent;
  let fixture: ComponentFixture<SideDrawerLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SideDrawerLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SideDrawerLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
