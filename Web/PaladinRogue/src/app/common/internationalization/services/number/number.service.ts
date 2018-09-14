import { Injectable } from '@angular/core';

import { NumberFormat } from '../number-format/number-format';
import { NumberFormatPart } from './constants/number-format-part.constant';

@Injectable()
export class NumberService {
  private readonly _numberFormat: NumberFormat;

  public constructor(numberFormat: NumberFormat) {
    this._numberFormat = numberFormat;
  }

  public format(number: number, precision?: number): string {
    const decimalSeparator: string = this._numberFormat.getDecimalSeparator(),
      groupSeparator: string = this._numberFormat.getGroupSeparator(),
      decimalFormat: string = this._numberFormat.getDecimalFormat(),
      numberToFixed: string = number.toFixed(precision),
      groupLength: number = decimalFormat.substring(
        decimalFormat.lastIndexOf(NumberFormatPart.GROUP) + 1,
        decimalFormat.lastIndexOf(NumberFormatPart.DECIMAL)
      ).length,
      containsDecimal: boolean = numberToFixed.indexOf(NumberFormatPart.DECIMAL) !== -1,
      decimalPart: string = containsDecimal ? numberToFixed.substr(
        numberToFixed.indexOf(NumberFormatPart.DECIMAL) + 1
      ) : '',
      wholePart: string = containsDecimal ? numberToFixed.substr(
        0,
        numberToFixed.indexOf(NumberFormatPart.DECIMAL)
      ) : numberToFixed;

    return `${
      wholePart.splitIntoLengthParts(groupLength, true).join(groupSeparator)
      }${
      (containsDecimal ? `${ decimalSeparator }${ decimalPart }` : '')
      }`;
  }
}
