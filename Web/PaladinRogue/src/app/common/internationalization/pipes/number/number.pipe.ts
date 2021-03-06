import { OnDestroy, Pipe, PipeTransform } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { LocaleService } from '../../services/locale/locale.service';
import { NumberService } from '../../services/number/number.service';

@Pipe({
  name: 'prNumber'
})
export class NumberPipe implements PipeTransform, OnDestroy {
  private readonly _numberService: NumberService;
  private readonly _localeService: LocaleService;
  private readonly _transformObservable: Observable<string>;
  private readonly _transformSubject: Subject<string>;

  private readonly _onDestroy: Subject<void> = new Subject();

  private _number: number;
  private _precision: number;

  public constructor(numberService: NumberService,
                     localeService: LocaleService) {
    this._numberService = numberService;
    this._localeService = localeService;
    this._transformSubject = new BehaviorSubject<string>('');

    this._localeService.localeChanged$.pipe(
      takeUntil(this._onDestroy)
    ).subscribe(() => {
      this._transformSubject.next(this._numberService.format(this._number, this._precision));
    });

    this._transformObservable = this._transformSubject.asObservable();
  }

  public transform(number: number, precision: number): Observable<string> {
    this._number = number;
    this._precision = precision;

    this._transformSubject.next(this._numberService.format(this._number, this._precision));

    return this._transformObservable;
  }

  public ngOnDestroy(): void {
    this._onDestroy.next();
  }
}
