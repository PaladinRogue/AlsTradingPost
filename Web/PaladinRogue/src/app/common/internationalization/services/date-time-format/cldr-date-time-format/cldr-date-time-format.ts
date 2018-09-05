import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import '../../../../text/string-extensions/string-extensions';
import {
  LdmlToMomentDateFormatAdapter
} from '../../date-format-adapters/ldml-moment-date-format-adapter/ldml-to-moment-date-format.adapter';
import { DateFormat } from '../../date-format/date-format';
import { TimeFormat } from '../../time-format/time-format';
import { DateTimeFormat } from '../date-time-format';
import { DateTimeFormatKey, IDateTimeFormatMap } from '../interfaces/date-time-format-map.interface';
import { ICldrDateTimeFormat } from './interfaces/cldr-date-time-format.interface';

@Injectable()
export class CldrDateTimeFormat implements DateTimeFormat {
  private _dateTimeFormat: IDateTimeFormatMap;

  private readonly _httpClient: HttpClient;
  private readonly _cldrDateTimeFormatBasePath: string;
  private readonly _dateFormat: DateFormat;
  private readonly _timeFormat: TimeFormat;
  private readonly _ldmlToMomentDateFormatAdapter: LdmlToMomentDateFormatAdapter;

  constructor(httpClient: HttpClient,
              cldrDateTimeFormatBasePath: string,
              dateFormat: DateFormat,
              timeFormat: TimeFormat) {
    this._httpClient = httpClient;
    this._cldrDateTimeFormatBasePath = cldrDateTimeFormatBasePath;
    this._dateFormat = dateFormat;
    this._timeFormat = timeFormat;
    this._ldmlToMomentDateFormatAdapter = LdmlToMomentDateFormatAdapter.create();
  }

  public getFullDateTimeFormat(): string {
    return this._getFormat('full').format(this._timeFormat.getFullTimeFormat(), this._dateFormat.getFullDateFormat());
  }

  public getLongDateTimeFormat(): string {
    return this._getFormat('long').format(this._timeFormat.getLongTimeFormat(), this._dateFormat.getLongDateFormat());
  }

  public getMediumDateTimeFormat(): string {
    return this._getFormat('medium').format(this._timeFormat.getMediumTimeFormat(), this._dateFormat.getMediumDateFormat());
  }

  public getShortDateTimeFormat(): string {
    return this._getFormat('short').format(this._timeFormat.getShortTimeFormat(), this._dateFormat.getShortDateFormat());
  }

  public async setLocale(localeId: string): Promise<void> {
    this._setDateTimeFormat(localeId, await this._getDateTimeFormatFromRegion(localeId));
  }

  private _getFormat(numberFormatKey: DateTimeFormatKey): string {
    return this._dateTimeFormat[numberFormatKey];
  }

  private _setDateTimeFormat(regionId: string, dateTimeFormat: ICldrDateTimeFormat): void {
    this._dateTimeFormat = {
      full: this._ldmlToMomentDateFormatAdapter.parse(dateTimeFormat.main[regionId].dates.calendars.gregorian.dateTimeFormats.full),
      long: this._ldmlToMomentDateFormatAdapter.parse(dateTimeFormat.main[regionId].dates.calendars.gregorian.dateTimeFormats.long),
      medium: this._ldmlToMomentDateFormatAdapter.parse(dateTimeFormat.main[regionId].dates.calendars.gregorian.dateTimeFormats.medium),
      short: this._ldmlToMomentDateFormatAdapter.parse(dateTimeFormat.main[regionId].dates.calendars.gregorian.dateTimeFormats.short)
    };
  }

  private _getDateTimeFormatFromRegion(regionId: string): Promise<ICldrDateTimeFormat> {
    return this._httpClient.get<ICldrDateTimeFormat>(`${ this._cldrDateTimeFormatBasePath }/main/${ regionId }/ca-gregorian.json`)
      .toPromise()
      .catch(() => {
        throw new Error('Cannot load cldr date time formats');
      });
  }
}
