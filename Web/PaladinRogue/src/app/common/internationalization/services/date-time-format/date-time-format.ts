import { ILocaleDependant } from '../locale/interfaces/locale-dependant.interface';

export abstract class DateTimeFormat implements ILocaleDependant {
  public abstract setLocale(regionId: string): Promise<void>;

  public abstract getFullDateTimeFormat(): string;

  public abstract getLongDateTimeFormat(): string;

  public abstract getMediumDateTimeFormat(): string;

  public abstract getShortDateTimeFormat(): string;
}
