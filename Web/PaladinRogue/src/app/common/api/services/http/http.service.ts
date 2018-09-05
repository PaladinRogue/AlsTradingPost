import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { IHttpResponse } from '../../interfaces/http-response/http-response.interface';
import { IResourceData } from '../../interfaces/resource-data/resource-data.interface';
import { IHttpRequest } from '../http-request/http-request.interface';

@Injectable()
export class HttpService {
  private _httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this._httpClient = httpClient;
  }

  public post(httpRequest: IHttpRequest<IResourceData>): Promise<IHttpResponse<IResourceData>> {
    return undefined;
  }

  public get(httpRequest: IHttpRequest<IResourceData>): Promise<IHttpResponse<IResourceData>> {
    return undefined;
  }

  public put(httpRequest: IHttpRequest<IResourceData>): Promise<IHttpResponse<IResourceData>> {
    return undefined;
  }

  public async delete(httpRequest: IHttpRequest<IResourceData>): Promise<IHttpResponse<IResourceData>> {
    await this._httpClient.delete(link.getUrl()).toPromise();

    return;
  }
}
