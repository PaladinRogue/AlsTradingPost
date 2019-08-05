import { ErrorType } from '../../constants/error-type/error-type.constant';

export class DataError {
  private readonly _errorType: ErrorType;

  private constructor(errorType: ErrorType) {
    this._errorType = errorType;
  }

  public get errorType(): ErrorType {
    return this._errorType;
  }

  public static create(errorType: ErrorType): DataError {
    return new DataError(errorType);
  }
}
