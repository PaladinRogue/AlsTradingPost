import { Injectable } from '@angular/core';
import { map } from 'lodash';
import { Observable, Subject } from 'rxjs';

import { ILocaleDependant } from './interfaces/locale-dependant.interface';

@Injectable()
export class LocaleService {
  public localeChanged$: Observable<string>;

  private readonly _localeChangedSubject: Subject<string>;
  private readonly _localeDependantServices: Array<ILocaleDependant> = [];

  private _localeId: string;

   public constructor() {
    this._localeChangedSubject = new Subject<string>();
    this.localeChanged$ = this._localeChangedSubject.asObservable();
  }

  public getLocale(): string {
    return this._localeId;
  }

  public async setLocale(localeId: string): Promise<void> {
     this._localeId = localeId;

    const setLocalePromises: Array<Promise<void>> = map(this._localeDependantServices, (service: ILocaleDependant): Promise<void> => {
      return service.setLocale(localeId);
    });

    await Promise.all(setLocalePromises);

    this._localeChangedSubject.next(localeId);
  }

  public addLocaleDependant(service: ILocaleDependant): void {
    this._localeDependantServices.push(service);
  }
}
