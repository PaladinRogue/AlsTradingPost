import { OnDestroy, Pipe, PipeTransform } from '@angular/core';
import { Moment } from 'moment';
import { BehaviorSubject, Observable, Subject, Unsubscribable } from 'rxjs';

import { DateFormatType } from '../../services/date/constants/date-format-type.constant';
import { DateService } from '../../services/date/date.service';
import { LocaleService } from '../../services/locale/locale.service';
import { TimezoneService } from '../../services/timezone/timezone.service';

@Pipe({
  name: 'prTime'
})
export class TimePipe implements PipeTransform, OnDestroy {
  private readonly _dateService: DateService;
  private readonly _localeService: LocaleService;
  private readonly _timezoneService: TimezoneService;
  private readonly _transformObservable: Observable<string>;
  private readonly _localeSubscription: Unsubscribable;
  private readonly _timezoneSubscription: Unsubscribable;
  private readonly _transformSubject: Subject<string>;

  private _date: Moment;
  private _format: DateFormatType;

  constructor(dateService: DateService,
              localeService: LocaleService,
              timezoneService: TimezoneService) {
    this._dateService = dateService;
    this._localeService = localeService;
    this._timezoneService = timezoneService;
    this._transformSubject = new BehaviorSubject<string>('');

    this._localeSubscription = this._localeService.localeChanged$.subscribe(() => {
      this._updateDate();
    });

    this._timezoneSubscription = this._timezoneService.timezoneChanged$.subscribe(() => {
      this._updateDate();
    });

    this._transformObservable = this._transformSubject.asObservable();
  }

  public transform(date: Moment, format: DateFormatType): Observable<string> {
    this._date = date;
    this._format = format;

    this._updateDate();

    return this._transformObservable;
  }

  public ngOnDestroy(): void {
    this._localeSubscription.unsubscribe();
    this._timezoneSubscription.unsubscribe();
  }

  private _updateDate(): void {
    this._transformSubject.next(this._dateService.formatTime(this._date, this._format));
  }
}
