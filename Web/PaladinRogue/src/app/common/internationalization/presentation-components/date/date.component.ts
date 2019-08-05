import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Moment } from 'moment';

import { DateFormatType } from '../../services/date/constants/date-format-type.constant';

@Component({
  selector: 'pr-date',
  templateUrl: './date.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DateComponent {
  @Input()
  public prDate: Moment;

  @Input()
  public prDateFormat: DateFormatType;
}
