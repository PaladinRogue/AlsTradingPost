import { Injectable } from '@angular/core';
import { reduce } from 'lodash';

import { ICollectionResourceResponse } from '../../interfaces/collection-resource-response/collection-resource-response.interface';
import { IErrorResponse } from '../../interfaces/error-response/error-response.interface';
import { IHttpResponse } from '../../interfaces/http-response/http-response.interface';
import { IResourceResponse } from '../../interfaces/resource-response/resource-response.interface';
import { HttpErrorHandlersProvider } from '../http-error-handlers-provider/http-error-handlers.provider';
import { IHttpErrorHandler } from '../http-error-handlers/interfaces/http-error-handler.interface';
import { HttpError } from '../http-error/http-error';
import { HttpRequestFactory } from '../http-request-factory/http-request-factory.service';
import { HttpService } from '../http/http.service';

@Injectable()
export class HttpApiService {
  private readonly _httpService: HttpService;
  private readonly _httpRequestFactory: HttpRequestFactory;
  private readonly _httpErrorHandlersProviderService: HttpErrorHandlersProvider;

  constructor(httpService: HttpService,
              httpRequestFactory: HttpRequestFactory,
              httpErrorHandlersProviderService: HttpErrorHandlersProvider) {
    this._httpService = httpService;
    this._httpRequestFactory = httpRequestFactory;
    this._httpErrorHandlersProviderService = httpErrorHandlersProviderService;
  }

  public async post(url: string): Promise<IResourceResponse> {
    let response: IHttpResponse<IResourceResponse>;

    try {
      response = await this._httpService.json<IResourceResponse>(this._httpRequestFactory.createGet(url));

      return response.body;
    } catch (e) {
      this._handleErrors(e);
    }
  }

  public async get(url: string): Promise<IResourceResponse | ICollectionResourceResponse> {
    let response: IHttpResponse<IResourceResponse | ICollectionResourceResponse>;

    try {
      response = await this._httpService.json<IResourceResponse | ICollectionResourceResponse>(this._httpRequestFactory.createGet(url));

      return response.body;
    } catch (e) {
      this._handleErrors(e);
    }
  }

  public async put(url: string): Promise<IResourceResponse> {
    let response: IHttpResponse<IResourceResponse>;

    try {
      response = await this._httpService.json<IResourceResponse>(this._httpRequestFactory.createGet(url));

      return response.body;
    } catch (e) {
      this._handleErrors(e);
    }
  }

  public async delete(url: string): Promise<void> {
    try {
      await this._httpService.json(this._httpRequestFactory.createGet(url));
    } catch (e) {
      this._handleErrors(e);
    }
  }

  private _handleErrors(errorResponse: IErrorResponse<any>): HttpError {
    return reduce(
      this._httpErrorHandlersProviderService.getForStatusCode(errorResponse.statusCode),
      (httpError: HttpError, errorHandler: IHttpErrorHandler): HttpError => {
        return errorHandler.handle(httpError);
      },
      HttpError.create(errorResponse));
  }
}
