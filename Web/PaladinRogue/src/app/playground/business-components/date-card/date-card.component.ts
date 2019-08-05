import { ChangeDetectionStrategy, Component } from '@angular/core';
import * as moment from 'moment';
import { Moment } from 'moment';

@Component({
  selector: 'pr-date-card',
  templateUrl: './date-card.component.html',
  styleUrls: ['./date-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DateCardComponent {
  public date: Moment = moment();
}
