import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { IDict } from '../../../core';
import { IResource } from '../../interfaces/resource/resource.interface';
import { Link } from '../link/link';

@Injectable()
export class ApiService {
  private _httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this._httpClient = httpClient;
  }

  public post(link: Link, data: IDict<any>): Promise<IResource> {
    return undefined;
  }

  public get(link: Link): Promise<IResource> {
    return undefined;
  }

  public put(link: Link, data: any): Promise<IResource> {
    return undefined;
  }

  public async delete(link: Link): Promise<void> {
    await this._httpClient.delete(link.getUrl()).toPromise();

    return;
  }
}
