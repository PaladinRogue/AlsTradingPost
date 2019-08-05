export interface ICldrDateTimeFormat {
  main: {
    [regionId: string]: {
      dates: {
        calendars: {
          gregorian: {
            dateTimeFormats: {
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
