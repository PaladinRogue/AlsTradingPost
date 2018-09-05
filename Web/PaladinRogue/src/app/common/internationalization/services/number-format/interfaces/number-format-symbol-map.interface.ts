export type NumberFormatSymbolKey = keyof INumberFormatSymbolMap;

export interface INumberFormatSymbolMap {
  decimal: string;
  group: string;
  percent: string;
}
