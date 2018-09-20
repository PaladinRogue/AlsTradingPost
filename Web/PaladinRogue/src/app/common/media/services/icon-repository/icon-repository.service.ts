import { IIconDefinition } from './interfaces/icon-definition.interface';

export abstract class IconRepository {
  public abstract addIcon(...icons: Array<IIconDefinition>): void;
}
