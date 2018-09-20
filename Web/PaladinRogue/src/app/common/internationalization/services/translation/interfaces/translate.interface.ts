import { ITranslateOptions } from './translate-options.interface';

export interface ITranslate {
  translateId: string;
  translateValues?: ITranslateOptions;
}
