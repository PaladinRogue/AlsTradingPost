export type DateFormatKey = keyof IDateFormatMap;

export interface IDateFormatMap {
  full: string;
  long: string;
  medium: string;
  short: string;
}
