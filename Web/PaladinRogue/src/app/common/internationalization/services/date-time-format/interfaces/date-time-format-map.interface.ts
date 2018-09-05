export type DateTimeFormatKey = keyof IDateTimeFormatMap;

export interface IDateTimeFormatMap {
  full: string;
  long: string;
  medium: string;
  short: string;
}
