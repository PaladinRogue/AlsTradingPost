import { HttpMethod } from '../../constants/http-method/http-method.constant';
import { HttpHeaders } from '../../services/http-headers/http-headers';

export interface IHttpRequest<T> {
  method: HttpMethod;
  url: string;
  body?: T;
  requiresAuth?: boolean;
  headers?: HttpHeaders;
}
