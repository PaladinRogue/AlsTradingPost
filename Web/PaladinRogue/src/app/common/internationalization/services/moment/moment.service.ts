import { Injectable } from '@angular/core';
import * as moment from 'moment-timezone';

import { ILocaleDependant } from '../locale/interfaces/locale-dependant.interface';
import { ITimezoneDependant } from '../timezone/interfaces/timezone-dependant.interface';

@Injectable()
export class MomentService implements ILocaleDependant, ITimezoneDependant {
  public setLocale(regionId: string): Promise<void> {
    moment.locale(regionId);

    return Promise.resolve();
  }

  public setTimezone(timezoneId: string): Promise<void> {
    moment.tz.setDefault(timezoneId);

    return Promise.resolve();
  }
}
