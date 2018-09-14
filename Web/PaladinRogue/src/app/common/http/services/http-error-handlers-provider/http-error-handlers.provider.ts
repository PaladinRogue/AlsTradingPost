import { Injectable } from '@angular/core';
import { filter } from 'lodash';

import { StatusCode } from '../../constants/status-code/status-code.constant';
import { HttpErrorHandlersService } from '../http-error-handlers/http-error-handlers.service';
import { IHttpErrorHandler } from '../http-error-handlers/interfaces/http-error-handler.interface';

@Injectable()
export class HttpErrorHandlersProvider {
  private readonly _httpErrorHandlersService: HttpErrorHandlersService;

  public constructor(httpErrorHandlersService: HttpErrorHandlersService) {
    this._httpErrorHandlersService = httpErrorHandlersService;
  }

  public getForStatusCode(statusCode: StatusCode): Array<IHttpErrorHandler> {
    return filter(this._httpErrorHandlersService.getAll(), { statusCode });
  }
}
