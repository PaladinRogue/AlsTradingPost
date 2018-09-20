import { Component, ChangeDetectionStrategy } from '@angular/core';
import * as moment from 'moment';
import { Moment } from 'moment';

@Component({
  selector: 'pr-date-page',
  templateUrl: './date-time-page.component.html',
  styleUrls: ['./date-time-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DateTimePageComponent {
  public date: Moment = moment();
}
