import { Component, ChangeDetectionStrategy } from '@angular/core';
import * as moment from 'moment';
import { Moment } from 'moment';

@Component({
  selector: 'pr-date-time-card',
  templateUrl: './date-time-card.component.html',
  styleUrls: ['./date-time-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DateTimeCardComponent {
  public date: Moment = moment();
}
