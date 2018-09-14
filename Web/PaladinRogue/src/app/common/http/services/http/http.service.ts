import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Injectable, isDevMode } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { IHttpRequest } from '../../interfaces/http-request/http-request.interface';
import { IHttpResponse } from '../../interfaces/http-response/http-response.interface';

@Injectable()
export class HttpService {
  private readonly _httpClient: HttpClient;

  public constructor(httpClient: HttpClient) {
    this._httpClient = httpClient;
  }

  public json<T>(httpRequest: IHttpRequest<any>): Promise<IHttpResponse<T>> {
    return this._httpClient.request<T>(httpRequest.method, httpRequest.url, {
      body: httpRequest.body,
      headers: httpRequest.headers.getAll(),
      reportProgress: false,
      withCredentials: true,
      responseType: 'json',
      observe: 'response'
    }).pipe(
      catchError((httpErrorResponse: HttpErrorResponse) => {
        if (isDevMode()) {
          console.error('JSON request failed', httpErrorResponse);
        }

        return throwError({
          body: httpErrorResponse.error,
          statusCode: httpErrorResponse.status,
          headers: httpErrorResponse.headers
        });
      }),
      map((httpResponse: HttpResponse<T>): IHttpResponse<T> => {
        return {
          body: httpResponse.body,
          status: httpResponse.status,
          headers: httpResponse.headers
        };
      })
    ).toPromise();
  }
}
