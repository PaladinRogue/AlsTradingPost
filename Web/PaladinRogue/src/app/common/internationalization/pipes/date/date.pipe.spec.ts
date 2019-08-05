import { Component } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Moment } from 'moment';

import { DateFormatType } from '../../services/date/constants/date-format-type.constant';
import { DatePipe } from './date.pipe';

describe('DatePipe', () => {
  let testHostFixture: ComponentFixture<TestHostComponent>;

  @Component({
    template: `{{ date | prDate:format }}`
  })
  class TestHostComponent {
    public date: Moment;
    public format: DateFormatType;
  }

  beforeEach(async(async() => {
    await TestBed.configureTestingModule({
      declarations: [DatePipe]
    }).compileComponents();

    testHostFixture = TestBed.createComponent(TestHostComponent);
  }));
});
