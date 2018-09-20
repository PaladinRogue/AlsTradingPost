import { Component, ChangeDetectionStrategy } from '@angular/core';
import * as moment from 'moment';
import { Moment } from 'moment';

@Component({
  selector: 'pr-date-page',
  templateUrl: './time-page.component.html',
  styleUrls: ['./time-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TimePageComponent {
  public date: Moment = moment();
}
