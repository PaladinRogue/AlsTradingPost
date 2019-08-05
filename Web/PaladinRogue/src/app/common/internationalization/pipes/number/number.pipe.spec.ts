import { Component } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NumberPipe } from './number.pipe';

describe('NumberPipe', () => {
  let testHostFixture: ComponentFixture<TestHostComponent>;

  @Component({
    template: `{{ number | prNumber:precision }}`
  })
  class TestHostComponent {
    public number: number;
    public precision: number;
  }

  beforeEach(async(async() => {
    await TestBed.configureTestingModule({
      declarations: [NumberPipe]
    }).compileComponents();

    testHostFixture = TestBed.createComponent(TestHostComponent);
  }));
});
