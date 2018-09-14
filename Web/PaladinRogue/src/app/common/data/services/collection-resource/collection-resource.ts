import { get, has } from 'lodash';
import { IDict } from '../../../core';

import { ILink } from '../link/link.interface';
import { ICollectionResource } from './collection-resource.interface';

export class CollectionResource implements ICollectionResource {
  private readonly _links: IDict<ILink>;

  private constructor(links: IDict<ILink>) {
    this._links = links;

    //   reduce<IDict<ILinkData>, IDict<ILink>>(
    //   resourceResponse.links,
    //   (accumulator: IDict<ILink>, linkData: ILinkData, key: string): IDict<ILink> => {
    //     accumulator[key] = Link.create(linkData);
    //
    //     return accumulator;
    //   }, {}
    // );
  }

  public static create(links: IDict<ILink>): ICollectionResource {
    return new CollectionResource(links);
  }

  public getLink(linkName: string): ILink {
    return get(this._links, linkName, null);
  }

  public hasLink(linkName: string): boolean {
    return has(this._links, linkName);
  }
}
