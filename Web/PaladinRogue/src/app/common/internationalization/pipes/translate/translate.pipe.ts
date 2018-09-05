import { OnDestroy, Pipe, PipeTransform } from '@angular/core';
import { BehaviorSubject, Observable, Subject, Unsubscribable } from 'rxjs';

import { LanguageService } from '../../services/language/language.service';
import { TranslateService } from '../../services/translate/translate.service';
import { ITranslate } from '../../services/translation/interfaces/translate.interface';

@Pipe({
  name: 'prTranslate'
})
export class TranslatePipe implements PipeTransform, OnDestroy {
  private readonly _translateService: TranslateService;
  private readonly _languageService: LanguageService;
  private readonly _languageSubscription: Unsubscribable;
  private readonly _transformObservable: Observable<string>;
  private readonly _transformSubject: Subject<string>;

  private _value: ITranslate;

  constructor(translationService: TranslateService,
              languageService: LanguageService) {
    this._translateService = translationService;
    this._languageService = languageService;
    this._transformSubject = new BehaviorSubject<string>('');

    this._languageSubscription = this._languageService.languageChanged$.subscribe(() => {
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
    this._languageSubscription.unsubscribe();
  }

  private _updateTranslation(): void {
    this._transformSubject.next(this._translateService.fromTranslation(this._value));
  }
}
