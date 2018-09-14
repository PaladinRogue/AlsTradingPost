import { StatusCode } from '../../constants/status-code/status-code.constant';
import { IHttpHeaders } from '../../services/http-headers/http-headers.interface';

export interface IHttpResponse<T> {
  body?: T;
  status: StatusCode;
  headers: IHttpHeaders;
}
