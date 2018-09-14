import { reduce } from 'lodash';

export class LdmlToMomentDateFormatAdapter {
  private readonly _tokensMap: { [ldmlToken: string]: string } = {
    yyyy: 'YYYY',
    yy: 'YY',
    y: 'YYYY',
    L: 'M',
    LL: 'MM',
    LLL: 'MMM',
    LLLL: 'MMMM',
    d: 'D',
    dd: 'DD',
    D: 'DDD',
    DDD: 'DDDD',
    E: 'ddd',
    EE: 'ddd',
    EEE: 'ddd',
    eee: 'ddd',
    EEEE: 'dddd',
    eeee: 'dddd',
    e: 'd',
    ee: 'd',
    a: 'A',
    k: 'H',
    kk: 'HH',
    K: 'h',
    KK: 'hh',
    zzz: 'zz',
    zzzz: 'zz'
  };

  public static create(): LdmlToMomentDateFormatAdapter {
    return new LdmlToMomentDateFormatAdapter();
  }

  public parse(input: string): string {
    const regex: RegExp = new RegExp(`([a-z]+|'.*(?=')')`, 'gi'),
      ldmlDateFormatParts: Array<string> = input.split(regex);

    return reduce(ldmlDateFormatParts, (momentDateFormatParts: Array<string>, ldmlDateFormatPart: string) => {
      momentDateFormatParts.push(this._tokensMap[ldmlDateFormatPart] || this._escapeStrings(ldmlDateFormatPart));

      return momentDateFormatParts;
    }, []).join('');
  }

  private _escapeStrings(ldmlDateFormatPart: string): string {
    const doubleQuoteRegex: RegExp = new RegExp(`'(?=')`, 'gi'),
      escapedStartStringRegex: RegExp = new RegExp(`^'`, 'gi'),
      escapedEndStringRegex: RegExp = new RegExp(`'$`, 'gi');
    return ldmlDateFormatPart.replace(doubleQuoteRegex, '').replace(escapedStartStringRegex, '[').replace(escapedEndStringRegex, ']');
  }
}
