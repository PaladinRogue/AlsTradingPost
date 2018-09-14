import { ILink } from '../link/link.interface';

export interface ICollectionResource {
  hasLink(linkName: string): boolean;

  getLink(linkName: string): ILink;
}
