import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { Moment } from 'moment';

import { DateFormat } from '../date-format/date-format';
import { DateTimeFormat } from '../date-time-format/date-time-format';
import { TimeFormat } from '../time-format/time-format';
import { TimezoneService } from '../timezone/timezone.service';
import { DateFormatType } from './constants/date-format-type.constant';

@Injectable()
export class DateService {
  private readonly _dateFormat: DateFormat;
  private readonly _timeFormat: TimeFormat;
  private readonly _dateTimeFormat: DateTimeFormat;
  private readonly _timezoneService: TimezoneService;

  public constructor(dateFormat: DateFormat,
                     timeFormat: TimeFormat,
                     dateTimeFormat: DateTimeFormat,
                     timezoneService: TimezoneService) {
    this._dateFormat = dateFormat;
    this._timeFormat = timeFormat;
    this._dateTimeFormat = dateTimeFormat;
    this._timezoneService = timezoneService;
  }

  public formatDate(date: Moment, format: DateFormatType): string {
    date.locale(moment.locale()).tz(this._timezoneService.getTimezone());

    switch (format) {
      case DateFormatType.FULL:
        return date.format(this._dateFormat.getFullDateFormat());
      case DateFormatType.LONG:
        return date.format(this._dateFormat.getLongDateFormat());
      case DateFormatType.MEDIUM:
        return date.format(this._dateFormat.getMediumDateFormat());
      default: // PipeFormatType.SHORT:
        return date.format(this._dateFormat.getShortDateFormat());
    }
  }

  public formatTime(time: Moment, format: DateFormatType): string {
    time.locale(moment.locale()).tz(this._timezoneService.getTimezone());

    switch (format) {
      case DateFormatType.FULL:
        return time.format(this._timeFormat.getFullTimeFormat());
      case DateFormatType.LONG:
        return time.format(this._timeFormat.getLongTimeFormat());
      case DateFormatType.MEDIUM:
        return time.format(this._timeFormat.getMediumTimeFormat());
      default: // PipeFormatType.SHORT:
        return time.format(this._timeFormat.getShortTimeFormat());
    }
  }

  public formatDateTime(dateTime: Moment, format: DateFormatType): string {
    dateTime.locale(moment.locale()).tz(this._timezoneService.getTimezone());

    switch (format) {
      case DateFormatType.FULL:
        return dateTime.format(this._dateTimeFormat.getFullDateTimeFormat());
      case DateFormatType.LONG:
        return dateTime.format(this._dateTimeFormat.getLongDateTimeFormat());
      case DateFormatType.MEDIUM:
        return dateTime.format(this._dateTimeFormat.getMediumDateTimeFormat());
      default: // PipeFormatType.SHORT:
        return dateTime.format(this._dateTimeFormat.getShortDateTimeFormat());
    }
  }
}
