import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Moment } from 'moment';

import { DateFormatType } from '../../services/date/constants/date-format-type.constant';

@Component({
  selector: 'pr-date-time',
  templateUrl: './date-time.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DateTimeComponent {
  @Input()
  public prDateTime: Moment;

  @Input()
  public prDateTimeFormat: DateFormatType;
}
