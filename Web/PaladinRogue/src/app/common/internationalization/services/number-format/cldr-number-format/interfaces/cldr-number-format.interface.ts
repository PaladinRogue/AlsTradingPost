export interface ICldrNumberFormat {
  main: {
    [regionId: string]: {
      identity: {
        language: string,
        territory: string
      },
      numbers: {
        'symbols-numberSystem-latn': {
          decimal: string,
          group: string,
          percentSign: string
        }
        'percentFormats-numberSystem-latn': {
          standard: string
        }
        'decimalFormats-numberSystem-latn': {
          standard: string
        }
      }
    }
  };
}
