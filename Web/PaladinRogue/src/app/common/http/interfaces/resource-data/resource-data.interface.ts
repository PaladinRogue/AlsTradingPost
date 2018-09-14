import { IDict } from '../../../core';

export interface IResourceData {
  id: string;
  attributes: IDict<any>;
  type: string;
  meta: IDict<IAttributeMeta>;
}
