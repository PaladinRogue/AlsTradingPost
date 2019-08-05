import { ILocaleDependant } from '../locale/interfaces/locale-dependant.interface';

export abstract class TimeFormat implements ILocaleDependant {
  public abstract setLocale(regionId: string): Promise<void>;

  public abstract getFullTimeFormat(): string;

  public abstract getLongTimeFormat(): string;

  public abstract getMediumTimeFormat(): string;

  public abstract getShortTimeFormat(): string;
}
