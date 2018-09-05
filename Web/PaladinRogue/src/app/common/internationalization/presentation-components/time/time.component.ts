import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Moment } from 'moment';

import { DateFormatType } from '../../services/date/constants/date-format-type.constant';

@Component({
  selector: 'pr-time',
  templateUrl: './time.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TimeComponent {
  @Input()
  public prTime: Moment;

  @Input()
  public prTimeFormat: DateFormatType;
}
