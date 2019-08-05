import { Inject, Injectable } from '@angular/core';
import { i18n, InitOptions } from 'i18next';
import * as Backend from 'i18next-xhr-backend';
import { ITranslate } from '../interfaces/translate.interface';
import { Translation } from '../translation';

import { i18nToken } from './tokens/i18n.token';
import { i18nConfigToken } from './tokens/i18nConfig.token';
import { i18nXHRToken } from './tokens/i18nXHR.token';

@Injectable()
export class I18nextTranslation implements Translation {
  private readonly _i18next: i18n;
  private readonly _translateConfig: InitOptions;

  public constructor(@Inject(i18nToken) i18next: i18n,
                     @Inject(i18nXHRToken) backend: Backend,
                     @Inject(i18nConfigToken) translateConfig: InitOptions) {
    this._i18next = i18next;
    this._translateConfig = translateConfig;

    this._i18next.use(backend);
  }

  public async setLanguage(languageId: string): Promise<void> {
    return new Promise<void>((resolve: Function): void => {
      this._i18next.on('loaded', () => {
        resolve();
      });
      this._i18next.init({ ...this._translateConfig, lng: languageId });
    });
  }

  public translate(translation: ITranslate): string {
    return this._i18next.t(translation.translateId, translation.translateValues);
  }
}
