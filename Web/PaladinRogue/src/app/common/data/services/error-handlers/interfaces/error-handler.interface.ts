import { ErrorType } from '../../../constants/error-type/error-type.constant';
import { DataError } from '../../data-error/data-error';

export interface IErrorHandler {
  errorType: ErrorType;

  handle(error: DataError): DataError;
}
