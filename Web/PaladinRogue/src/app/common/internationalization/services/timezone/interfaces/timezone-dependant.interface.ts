export interface ITimezoneDependant {
  setTimezone(timezone: string): Promise<void>;
}
