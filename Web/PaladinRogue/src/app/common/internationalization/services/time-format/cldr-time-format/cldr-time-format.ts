import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import {
  LdmlToMomentDateFormatAdapter
} from '../../date-format-adapters/ldml-moment-date-format-adapter/ldml-to-moment-date-format.adapter';
import { ITimeFormatMap, TimeFormatKey } from '../interfaces/time-format-map.interface';
import { TimeFormat } from '../time-format';
import { ICldrTimeFormat } from './interfaces/cldr-time-format.interface';

@Injectable()
export class CldrTimeFormat implements TimeFormat {
  private readonly _httpClient: HttpClient;
  private readonly _cldrTimeFormatBasePath: string;
  private readonly _ldmlToMomentDateFormatAdapter: LdmlToMomentDateFormatAdapter;

  private _timeFormat: ITimeFormatMap;

  public constructor(httpClient: HttpClient, cldrTimeFormatBasePath: string) {
    this._httpClient = httpClient;
    this._cldrTimeFormatBasePath = cldrTimeFormatBasePath;
    this._ldmlToMomentDateFormatAdapter = LdmlToMomentDateFormatAdapter.create();
  }

  public getFullTimeFormat(): string {
    return this._getFormat('full');
  }

  public getLongTimeFormat(): string {
    return this._getFormat('long');
  }

  public getMediumTimeFormat(): string {
    return this._getFormat('medium');
  }

  public getShortTimeFormat(): string {
    return this._getFormat('short');
  }

  public async setLocale(localeId: string): Promise<void> {
    this._setTimeFormat(localeId, await this._getTimeFormatFromRegion(localeId));
  }

  private _getFormat(numberFormatKey: TimeFormatKey): string {
    return this._timeFormat[numberFormatKey];
  }

  private _setTimeFormat(regionId: string, timeFormat: ICldrTimeFormat): void {
    this._timeFormat = {
      full: this._ldmlToMomentDateFormatAdapter.parse(timeFormat.main[regionId].dates.calendars.gregorian.timeFormats.full),
      long: this._ldmlToMomentDateFormatAdapter.parse(timeFormat.main[regionId].dates.calendars.gregorian.timeFormats.long),
      medium: this._ldmlToMomentDateFormatAdapter.parse(timeFormat.main[regionId].dates.calendars.gregorian.timeFormats.medium),
      short: this._ldmlToMomentDateFormatAdapter.parse(timeFormat.main[regionId].dates.calendars.gregorian.timeFormats.short)
    };
  }

  private _getTimeFormatFromRegion(regionId: string): Promise<ICldrTimeFormat> {
    return this._httpClient.get<ICldrTimeFormat>(`${ this._cldrTimeFormatBasePath }/main/${ regionId }/ca-gregorian.json`)
      .toPromise()
      .catch(() => {
        throw new Error('Cannot load cldr time formats');
      });
  }
}
