import { HttpClient } from '@angular/common/http';

import {
  LdmlToMomentDateFormatAdapter
} from '../../date-format-adapters/ldml-moment-date-format-adapter/ldml-to-moment-date-format.adapter';
import { DateFormat } from '../date-format';
import { DateFormatKey, IDateFormatMap } from '../interfaces/date-format-map.interface';
import { ICldrDateFormat } from './interfaces/cldr-date-format.interface';

export class CldrDateFormat implements DateFormat {
  private readonly _httpClient: HttpClient;
  private readonly _cldrDateFormatBasePath: string;
  private readonly _ldmlToMomentDateFormatAdapter: LdmlToMomentDateFormatAdapter;

  private _dateFormat: IDateFormatMap;

  public constructor(httpClient: HttpClient, cldrDateFormatBasePath: string) {
    this._httpClient = httpClient;
    this._cldrDateFormatBasePath = cldrDateFormatBasePath;
    this._ldmlToMomentDateFormatAdapter = LdmlToMomentDateFormatAdapter.create();
  }

  public getFullDateFormat(): string {
    return this._getFormat('full');
  }

  public getLongDateFormat(): string {
    return this._getFormat('long');
  }

  public getMediumDateFormat(): string {
    return this._getFormat('medium');
  }

  public getShortDateFormat(): string {
    return this._getFormat('short');
  }

  public async setLocale(localeId: string): Promise<void> {
    this._setDateFormat(localeId, await this._getDateFormatFromRegion(localeId));
  }

  private _getFormat(numberFormatKey: DateFormatKey): string {
    return this._dateFormat[numberFormatKey];
  }

  private _setDateFormat(regionId: string, dateFormat: ICldrDateFormat): void {
    this._dateFormat = {
      full: this._ldmlToMomentDateFormatAdapter.parse(dateFormat.main[regionId].dates.calendars.gregorian.dateFormats.full),
      long: this._ldmlToMomentDateFormatAdapter.parse(dateFormat.main[regionId].dates.calendars.gregorian.dateFormats.long),
      medium: this._ldmlToMomentDateFormatAdapter.parse(dateFormat.main[regionId].dates.calendars.gregorian.dateFormats.medium),
      short: this._ldmlToMomentDateFormatAdapter.parse(dateFormat.main[regionId].dates.calendars.gregorian.dateFormats.short)
    };
  }

  private _getDateFormatFromRegion(regionId: string): Promise<ICldrDateFormat> {
    return this._httpClient.get<ICldrDateFormat>(`${ this._cldrDateFormatBasePath }/main/${ regionId }/ca-gregorian.json`)
      .toPromise()
      .catch(() => {
        throw new Error('Cannot load cldr date formats');
      });
  }
}
