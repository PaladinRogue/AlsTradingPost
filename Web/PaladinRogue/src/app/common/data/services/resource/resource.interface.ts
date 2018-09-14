import { IDict } from '../../../core';

import { ILink } from '../link/link.interface';

export interface IResource {
  attributes: IDict<any>; // tslint:disable-line no-any
  hasLink(linkName: string): boolean;

  getLink(linkName: string): ILink;
}
