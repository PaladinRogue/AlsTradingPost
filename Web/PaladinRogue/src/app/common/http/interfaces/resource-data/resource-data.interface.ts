import { IDict } from '../../../core';

export interface IResourceData {
  id: string;
  attributes: IDict<any>; // tslint:disable-line no-any
  type: string;
  meta: IDict<IAttributeMeta>;
}
