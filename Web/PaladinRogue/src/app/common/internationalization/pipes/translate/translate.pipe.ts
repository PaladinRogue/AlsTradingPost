import { OnDestroy, Pipe, PipeTransform } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { LanguageService } from '../../services/language/language.service';
import { TranslateService } from '../../services/translate/translate.service';
import { ITranslate } from '../../services/translation/interfaces/translate.interface';

@Pipe({
  name: 'prTranslate'
})
export class TranslatePipe implements PipeTransform, OnDestroy {
  private readonly _translateService: TranslateService;
  private readonly _languageService: LanguageService;
  private readonly _transformObservable: Observable<string>;
  private readonly _transformSubject: Subject<string>;

  private readonly _onDestroy: Subject<void> = new Subject();

  private _value: ITranslate;

  public constructor(translationService: TranslateService,
                     languageService: LanguageService) {
    this._translateService = translationService;
    this._languageService = languageService;
    this._transformSubject = new BehaviorSubject<string>('');

    this._languageService.languageChanged$.pipe(
      takeUntil(this._onDestroy)
    ).subscribe(() => {
      this._updateTranslation();
    });

    this._transformObservable = this._transformSubject.asObservable();
  }

  public transform(value: ITranslate): Observable<string> {
    this._value = value;

    this._updateTranslation();

    return this._transformObservable;
  }

  public ngOnDestroy(): void {
    this._onDestroy.next();
  }

  private _updateTranslation(): void {
    this._transformSubject.next(this._translateService.fromTranslation(this._value));
  }
}
