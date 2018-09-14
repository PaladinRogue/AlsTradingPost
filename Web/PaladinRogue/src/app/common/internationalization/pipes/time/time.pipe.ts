import { OnDestroy, Pipe, PipeTransform } from '@angular/core';
import { Moment } from 'moment';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

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
  private readonly _transformSubject: Subject<string>;

  private readonly _onDestroy: Subject<void> = new Subject();

  private _date: Moment;
  private _format: DateFormatType;

  constructor(dateService: DateService,
              localeService: LocaleService,
              timezoneService: TimezoneService) {
    this._dateService = dateService;
    this._localeService = localeService;
    this._timezoneService = timezoneService;
    this._transformSubject = new BehaviorSubject<string>('');

    this._localeService.localeChanged$.pipe(
      takeUntil(this._onDestroy)
    ).subscribe(() => {
      this._updateDate();
    });

    this._timezoneService.timezoneChanged$.pipe(
      takeUntil(this._onDestroy)
    ).subscribe(() => {
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
    this._onDestroy.next();
  }

  private _updateDate(): void {
    this._transformSubject.next(this._dateService.formatTime(this._date, this._format));
  }
}
