import { Component, ChangeDetectionStrategy } from '@angular/core';
import * as moment from 'moment';
import { Moment } from 'moment';

@Component({
  selector: 'pr-time-card',
  templateUrl: './time-card.component.html',
  styleUrls: ['./time-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TimeCardComponent {
  public date: Moment = moment();
}
