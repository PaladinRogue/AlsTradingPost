import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ILocaleDependant } from '../../locale/interfaces/locale-dependant.interface';
import { INumberFormatMap, NumberFormatKey } from '../interfaces/number-format-map.interface';
import { INumberFormatSymbolMap, NumberFormatSymbolKey } from '../interfaces/number-format-symbol-map.interface';
import { NumberFormat } from '../number-format';
import { ICldrNumberFormat } from './interfaces/cldr-number-format.interface';

@Injectable()
export class CldrNumberFormat implements NumberFormat, ILocaleDependant {
  private readonly _httpClient: HttpClient;
  private readonly _cldrNumberFormatBasePath: string;

  private _numberFormat: INumberFormatMap;
  private _symbolFormat: INumberFormatSymbolMap;

  public constructor(httpClient: HttpClient, cldrNumberFormatBasePath: string) {
    this._httpClient = httpClient;
    this._cldrNumberFormatBasePath = cldrNumberFormatBasePath;
  }

  public getDecimalFormat(): string {
    return this._getFormat('decimal');
  }

  public getPercentFormat(): string {
    return this._getFormat('percent');
  }

  public getDecimalSeparator(): string {
    return this._getSymbol('decimal');
  }

  public getGroupSeparator(): string {
    return this._getSymbol('group');
  }

  public getPercentSign(): string {
    return this._getSymbol('percent');
  }

  public async setLocale(regionId: string): Promise<void> {
    this._setNumberFormat(regionId, await this._getNumberFormatFromRegion(regionId));
  }

  private _getSymbol(numberFormatSymbolKey: NumberFormatSymbolKey): string {
    return this._symbolFormat[numberFormatSymbolKey];
  }

  private _getFormat(numberFormatKey: NumberFormatKey): string {
    return this._numberFormat[numberFormatKey];
  }

  private _setNumberFormat(regionId: string, numberFormat: ICldrNumberFormat): void {
    this._numberFormat = {
      decimal: numberFormat.main[regionId].numbers['decimalFormats-numberSystem-latn'].standard,
      percent: numberFormat.main[regionId].numbers['percentFormats-numberSystem-latn'].standard,
    };

    this._symbolFormat = {
      decimal: numberFormat.main[regionId].numbers['symbols-numberSystem-latn'].decimal,
      group: numberFormat.main[regionId].numbers['symbols-numberSystem-latn'].group,
      percent: numberFormat.main[regionId].numbers['symbols-numberSystem-latn'].percentSign
    };
  }

  private _getNumberFormatFromRegion(regionId: string): Promise<ICldrNumberFormat> {
    return this._httpClient.get<ICldrNumberFormat>(`${ this._cldrNumberFormatBasePath }/main/${ regionId }/numbers.json`).toPromise();
  }
}
