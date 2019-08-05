import { HttpClient } from '@angular/common/http';
import { DateFormat } from './date-format';

export interface IDateFormat {
  new(httpClient: HttpClient, dateFormatBasePath: string): DateFormat;
}
