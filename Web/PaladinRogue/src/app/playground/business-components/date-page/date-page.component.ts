import { Component, ChangeDetectionStrategy } from '@angular/core';
import * as moment from 'moment';
import { Moment } from 'moment';

@Component({
  selector: 'pr-date-page',
  templateUrl: './date-page.component.html',
  styleUrls: ['./date-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DatePageComponent {
  public date: Moment = moment();
}
