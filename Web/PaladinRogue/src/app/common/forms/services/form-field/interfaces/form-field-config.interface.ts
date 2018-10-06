import { ITranslate } from '../../../../internationalization';
import { IValidator } from '../../../interfaces/validator.interface';

export interface IFormFieldConfig<T> {
  label: ITranslate;
  validators?: Array<IValidator>;
  placeholder?: ITranslate;
  isDisabled?: boolean;
  isRequired?: boolean;

  getValue(): T;

  setValue(value: T): void;
}
