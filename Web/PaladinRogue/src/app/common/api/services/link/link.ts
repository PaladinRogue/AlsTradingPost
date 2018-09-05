import { IDict } from '../../../core';

export class Link {
  private readonly _url: string;
  private readonly _params: IDict<any>;

  private constructor(url: string, params?: IDict<any>) {
    this._url = url;
    this._params = params;
  }

  public static create(url: string, params?: IDict<any>): Link {
    return new Link(url, params);
  }

  public getUrl(): string {
    return this._url;
  }
}
