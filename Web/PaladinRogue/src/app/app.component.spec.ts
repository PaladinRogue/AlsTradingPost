import { TestBed, async } from '@angular/core/testing';
import { ComponentFixture } from '@angular/core/testing/src/component_fixture';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let testHostFixture: ComponentFixture<AppComponent>;

  beforeEach(async(async() => {
    await TestBed.configureTestingModule({
      declarations: [
        AppComponent
      ],
    }).compileComponents();

    testHostFixture = TestBed.createComponent(AppComponent);
  }));

  it('should create the app', async(() => {
    expect(testHostFixture.debugElement.componentInstance).toBeTruthy();
  }));

  it(`should have as title 'PaladinRogue'`, async(() => {
    expect(testHostFixture.debugElement.componentInstance.title).toEqual('PaladinRogue');
  }));

  it('should render title in a h1 tag', async(() => {
    expect(testHostFixture.debugElement.nativeElement.querySelector('h1').textContent).toContain('Welcome to PaladinRogue!');
  }));
});
