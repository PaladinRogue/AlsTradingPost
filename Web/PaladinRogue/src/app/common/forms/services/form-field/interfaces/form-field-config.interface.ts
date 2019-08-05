import { FieldType } from '../../../constants/field-type.constant';
import { ITranslate } from '../../../../internationalization';
import { IValidator } from '../../../interfaces/validator.interface';

export interface IFormFieldConfig<T> {
  name: string;
  label: ITranslate;
  type?: FieldType;
  validators?: Array<IValidator>;
  placeholder?: ITranslate;
  isDisabled?: boolean;
  isRequired?: boolean;

  getValue(): T;

  setValue(value: T): void;
}
