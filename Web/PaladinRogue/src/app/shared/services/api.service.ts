import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private _httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this._httpClient = httpClient;
  }

  public post(url: string, data: any): Promise<any> {
    return undefined;
  }

  public get(url: string): Promise<any> {
    return undefined;
  }

  public put(url: string, data: any): Promise<any> {
    return undefined;
  }

  public delete(url: string): Promise<void> {
    return undefined;
  }
}
