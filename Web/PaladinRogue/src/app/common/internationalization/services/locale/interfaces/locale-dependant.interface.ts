export interface ILocaleDependant {
  setLocale(localeId: string): Promise<void>;
}
