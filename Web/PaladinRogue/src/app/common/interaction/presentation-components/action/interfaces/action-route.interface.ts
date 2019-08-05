import { IDict } from '../../../../core';

export interface IActionRoute {
  route: string;
  routeParams: IDict<any>;
}
