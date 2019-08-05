import { IDict } from '../../../core';
import { get } from 'lodash';

import { IHttpHeaders } from './http-headers.interface';

export class HttpHeaders implements IHttpHeaders {
  private readonly _httpHeaders: IDict<string> = {};

  private constructor() {}

  public set authorization(authorizationHeader: string) {
    this._httpHeaders.authorization = authorizationHeader;
  }

  public set contentType(contentTypeHeader: string) {
    this._httpHeaders['Content-Type'] = contentTypeHeader;
  }

  public set accept(acceptHeader: string) {
    this._httpHeaders.accept = acceptHeader;
  }

  public static create(): HttpHeaders {
    return new HttpHeaders();
  }

  public setHeader(headerName: string, headerValue: string | boolean): void {
    this._httpHeaders[headerName] = headerValue.toString();
  }

  public get(key: string): string {
    return get(this._httpHeaders, key);
  }

  public getAll(): IDict<string> {
    return this._httpHeaders;
  }
}
