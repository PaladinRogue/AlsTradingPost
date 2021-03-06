import { Injectable } from '@angular/core';
import { map } from 'lodash';
import { Observable, Subject } from 'rxjs';

import { ILanguageDependant } from './interfaces/language-dependant.interface';

@Injectable()
export class LanguageService {
  public languageChanged$: Observable<string>;

  private readonly _languageChangedSubject: Subject<string>;
  private readonly _languageDependantServices: Array<ILanguageDependant> = [];

  private _languageId: string;

  public constructor() {
    this._languageChangedSubject = new Subject<string>();
    this.languageChanged$ = this._languageChangedSubject.asObservable();
  }

  public getlanguage(): string {
    return this._languageId;
  }

  public async setLanguage(languageId: string): Promise<void> {
    this._languageId = languageId;

    const setLanguagePromises: Array<Promise<void>> = map(this._languageDependantServices, (service: ILanguageDependant): Promise<void> => {
      return service.setLanguage(languageId);
    });

    await Promise.all(setLanguagePromises);

    this._languageChangedSubject.next(languageId);
  }

  public addLanguageDependant(service: ILanguageDependant): void {
    this._languageDependantServices.push(service);
  }
}
