import { ILanguageDependant } from '../language/interfaces/language-dependant.interface';
import { ITranslate } from './interfaces/translate.interface';

export abstract class Translation implements ILanguageDependant {
  public abstract translate(translation: ITranslate): string;

  public abstract setLanguage(languageId: string): Promise<void>;
}
