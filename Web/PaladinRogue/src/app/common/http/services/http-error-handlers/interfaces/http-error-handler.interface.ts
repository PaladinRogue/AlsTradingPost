import { StatusCode } from '../../../constants/status-code/status-code.constant';
import { HttpError } from '../../http-error/http-error';

export interface IHttpErrorHandler {
  statusCode: StatusCode;
  handle(error: HttpError): HttpError;
}
