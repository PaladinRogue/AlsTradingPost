import { Injectable } from '@angular/core';

import { ITranslate } from '../translation/interfaces/translate.interface';
import { Translation } from '../translation/translation';

@Injectable()
export class TranslateService {
  private readonly _translation: Translation;

  constructor(translation: Translation) {
    this._translation = translation;
  }

  public fromTranslation(translation: ITranslate): string {
    return this._translation.translate(translation);
  }
}
