export type TimeFormatKey = keyof ITimeFormatMap;

export interface ITimeFormatMap {
  full: string;
  long: string;
  medium: string;
  short: string;
}
