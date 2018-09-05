import { ILocaleDependant } from '../locale/interfaces/locale-dependant.interface';

export abstract class NumberFormat implements ILocaleDependant {
  public abstract setLocale(regionId: string): Promise<void>;

  public abstract getDecimalFormat(): string;
  public abstract getPercentFormat(): string;

  public abstract getDecimalSeparator(): string;
  public abstract getGroupSeparator(): string;
  public abstract getPercentSign(): string;
}
