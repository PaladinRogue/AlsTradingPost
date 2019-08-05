import { StatusCode } from '../../constants/status-code/status-code.constant';
import { IErrorResponse } from '../../interfaces/error-response/error-response.interface';

export class HttpError {
  private readonly _errorResponse: IErrorResponse<any>;

  private constructor(errorResponse: IErrorResponse<any>) {
    this._errorResponse = errorResponse;
  }

  public get statusCode(): StatusCode {
    return this._errorResponse.statusCode;
  }

  public static create(errorResponse: IErrorResponse<any>): HttpError {
    return new HttpError(errorResponse);
  }
}
