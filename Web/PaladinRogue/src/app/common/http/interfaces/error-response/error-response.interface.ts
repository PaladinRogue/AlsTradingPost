import { StatusCode } from '../../constants/status-code/status-code.constant';
import { IHttpHeaders } from '../../services/http-headers/http-headers.interface';

export interface IErrorResponse<T> {
  body?: T;
  statusCode: StatusCode;
  headers: IHttpHeaders;
}
