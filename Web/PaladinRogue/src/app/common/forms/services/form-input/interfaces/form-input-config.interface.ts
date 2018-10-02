import { Observable } from 'rxjs';
import { ITranslate } from '../../../../internationalization';

import { InputType } from '../../../constants/input-type.constant';

export interface IFormInputConfig<T> {
  type: InputType;
  label: ITranslate;
  placeholder?: ITranslate;
  isDisabled: boolean | Observable<boolean>;

  getValue(): T;

  setValue(value: T): void;
}
