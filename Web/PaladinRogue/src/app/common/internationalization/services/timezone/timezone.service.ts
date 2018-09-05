import { Injectable } from '@angular/core';
import { map } from 'lodash';
import { Observable, Subject } from 'rxjs';

import { ITimezoneDependant } from './interfaces/timezone-dependant.interface';

@Injectable()
export class TimezoneService {
  public timezoneChanged$: Observable<string>;

  private readonly _timezoneChangedSubject: Subject<string>;
  private readonly _timezoneDependantServices: Array<ITimezoneDependant> = [];

  private _timezoneId: string;

  constructor() {
    this._timezoneChangedSubject = new Subject<string>();
    this.timezoneChanged$ = this._timezoneChangedSubject.asObservable();
  }

  public getTimezone(): string {
    return this._timezoneId;
  }

  public async setTimezone(timezoneId: string): Promise<void> {
    this._timezoneId = timezoneId;

    const setTimezonePromises: Array<Promise<void>> = map(this._timezoneDependantServices, (service: ITimezoneDependant): Promise<void> => {
      return service.setTimezone(timezoneId);
    });

    await Promise.all(setTimezonePromises);

    this._timezoneChangedSubject.next(timezoneId);
  }

  public addTimezoneDependant(service: ITimezoneDependant): void {
    this._timezoneDependantServices.push(service);
  }
}
