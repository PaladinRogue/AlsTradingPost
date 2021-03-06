import { IDict } from '../../../core';

import { ILinkData } from '../link-data/link-data.interface';
import { IResourceData } from '../resource-data/resource-data.interface';

export interface ICollectionResourceResponse {
  data: Array<IResourceData>;
  links: IDict<ILinkData>;
  meta: IResourceMeta;
}
