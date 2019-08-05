import { Component } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Moment } from 'moment';

import { DateFormatType } from '../../services/date/constants/date-format-type.constant';
import { DateTimePipe } from './date-time.pipe';

describe('DateTimePipe', () => {
  let testHostFixture: ComponentFixture<TestHostComponent>;

  @Component({
    template: `{{ date | prDateTime:format }}`
  })
  class TestHostComponent {
    public date: Moment;
    public format: DateFormatType;
  }

  beforeEach(async(async() => {
    await TestBed.configureTestingModule({
      declarations: [DateTimePipe]
    }).compileComponents();

    testHostFixture = TestBed.createComponent(TestHostComponent);
  }));
});
