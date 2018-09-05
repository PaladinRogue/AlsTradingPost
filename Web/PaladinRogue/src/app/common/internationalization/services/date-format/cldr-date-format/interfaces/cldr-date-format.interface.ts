export interface ICldrDateFormat {
  main: {
    [regionId: string]: {
      dates: {
        calendars: {
          gregorian: {
            dateFormats: {
              full: string;
              long: string;
              medium: string;
              short: string;
            }
          }
        }
      }
    }
  };
}
