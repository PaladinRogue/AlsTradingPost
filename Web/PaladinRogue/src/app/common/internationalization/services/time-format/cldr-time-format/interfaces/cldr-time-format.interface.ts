export interface ICldrTimeFormat {
  main: {
    [regionId: string]: {
      dates: {
        calendars: {
          gregorian: {
            timeFormats: {
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
