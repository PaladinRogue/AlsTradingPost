import { IDict } from '../../../core';

import { Method } from '../../constants/method/method.constant';
import { ILink } from './link.interface';

export class Link implements ILink {
  private readonly _href: string;
  private readonly _methods: Array<Method>;
  private readonly _params: IDict<any>;

  private constructor(href: string, methods: Array<Method>) {
    this._href = href;
    this._methods = methods;
  }

  public static create(href: string, ...methods: Array<Method>): Link {
    return new Link(href, methods);
  }

  public getUrl(): string {
    return this._href;
  }
}
