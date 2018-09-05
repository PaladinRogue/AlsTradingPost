import { ILocaleDependant } from '../locale/interfaces/locale-dependant.interface';

export abstract class DateFormat implements ILocaleDependant {
  public abstract setLocale(regionId: string): Promise<void>;

  public abstract getFullDateFormat(): string;

  public abstract getLongDateFormat(): string;

  public abstract getMediumDateFormat(): string;

  public abstract getShortDateFormat(): string;
}
