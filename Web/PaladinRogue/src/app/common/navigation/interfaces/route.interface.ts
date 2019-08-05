import { IDict } from '../../core';
import { ITranslate } from '../../internationalization';

export interface IRoute {
  label: ITranslate;
  route: string;
  routeParams: IDict<any>;
}
