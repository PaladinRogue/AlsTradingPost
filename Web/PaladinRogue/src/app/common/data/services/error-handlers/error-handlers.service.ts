import { Injectable } from '@angular/core';

import { IErrorHandler } from './interfaces/error-handler.interface';

@Injectable()
export class ErrorHandlersService {
  private readonly _httpErrorHandlers: Array<IErrorHandler>;

  public add(errorHandler: IErrorHandler): void {
    this._httpErrorHandlers.push(errorHandler);
  }

  public getAll(): Array<IErrorHandler> {
    return this._httpErrorHandlers;
  }
}
