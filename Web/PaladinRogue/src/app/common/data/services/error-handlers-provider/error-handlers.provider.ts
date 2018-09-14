import { Injectable } from '@angular/core';
import { filter } from 'lodash';

import { ErrorType } from '../../constants/error-type/error-type.constant';
import { ErrorHandlersService } from '../error-handlers/error-handlers.service';
import { IErrorHandler } from '../error-handlers/interfaces/error-handler.interface';

@Injectable()
export class ErrorHandlersProvider {
  private readonly _errorHandlersService: ErrorHandlersService;

  constructor(errorHandlersService: ErrorHandlersService) {
    this._errorHandlersService = errorHandlersService;
  }

  public getForErrorType(errorType: ErrorType): Array<IErrorHandler> {
    return filter(this._errorHandlersService.getAll(), { errorType });
  }
}
