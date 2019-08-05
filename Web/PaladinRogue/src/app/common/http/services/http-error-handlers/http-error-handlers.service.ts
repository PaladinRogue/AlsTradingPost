import { Injectable } from '@angular/core';

import { IHttpErrorHandler } from './interfaces/http-error-handler.interface';

@Injectable()
export class HttpErrorHandlersService {
  private readonly _httpErrorHandlers: Array<IHttpErrorHandler>;

  public add(httpErrorHandler: IHttpErrorHandler): void {
    this._httpErrorHandlers.push(httpErrorHandler);
  }

  public getAll(): Array<IHttpErrorHandler> {
    return this._httpErrorHandlers;
  }
}
