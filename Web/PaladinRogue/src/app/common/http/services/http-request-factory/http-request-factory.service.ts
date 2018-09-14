import { Injectable } from '@angular/core';
import { AuthorisationService } from '../../../security';

import { HttpMethod } from '../../constants/http-method/http-method.constant';
import { IHttpRequest } from '../../interfaces/http-request/http-request.interface';
import { HttpHeaders } from '../http-headers/http-headers';

@Injectable()
export class HttpRequestFactory {
  private readonly _responseType: string = 'application/vnd.api+json';

  private readonly _authorisationService: AuthorisationService;

  public constructor(authorisationService: AuthorisationService) {
    this._authorisationService = authorisationService;
  }

  public createGet(url: string, requiresAuth: boolean = true): IHttpRequest<void> {
    return {
      url,
      method: HttpMethod.GET,
      headers: this._getHeaders(requiresAuth, false),
      requiresAuth
    };
  }

  public createPost<TRequest>(url: string, body: TRequest, requiresAuth: boolean = true): IHttpRequest<TRequest> {
    return {
      url,
      method: HttpMethod.POST,
      headers: this._getHeaders(requiresAuth, true),
      body,
      requiresAuth
    };
  }

  public createPut<TRequest>(url: string, body: TRequest, requiresAuth: boolean = true): IHttpRequest<TRequest> {
    return {
      url,
      method: HttpMethod.PUT,
      headers: this._getHeaders(requiresAuth, true),
      body,
      requiresAuth
    };
  }

  public createDelete(url: string, requiresAuth: boolean = true): IHttpRequest<void> {
    return {
      url,
      method: HttpMethod.DELETE,
      headers: this._getHeaders(requiresAuth, false),
      requiresAuth
    };
  }

  private _getHeaders(requiresAuth: boolean, hasBody: boolean): HttpHeaders {
    const httpHeaders: HttpHeaders = HttpHeaders.create();

    httpHeaders.setHeader('Extended-Meta', true);
    httpHeaders.accept = this._responseType;

    if (hasBody) {
      httpHeaders.contentType = this._responseType;
    }

    if (requiresAuth) {
      httpHeaders.authorization = this._authorisationService.getAuthorisationHeader();
    }

    return httpHeaders;
  }
}
