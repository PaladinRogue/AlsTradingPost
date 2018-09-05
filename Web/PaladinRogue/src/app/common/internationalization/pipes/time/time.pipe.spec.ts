import { Component } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Moment } from 'moment';

import { DateFormatType } from '../../services/date/constants/date-format-type.constant';
import { TimePipe } from './time.pipe';

describe('TimePipe', () => {
  let testHostFixture: ComponentFixture<TestHostComponent>;

  @Component({
    template: `{{ date | prTime:format }}`
  })
  class TestHostComponent {
    public date: Moment;
    public format: DateFormatType;
  }

  beforeEach(async(async() => {
    await TestBed.configureTestingModule({
      declarations: [TimePipe]
    }).compileComponents();

    testHostFixture = TestBed.createComponent(TestHostComponent);
  }));
});
