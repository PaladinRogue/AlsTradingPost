import { FieldType } from '../../../constants/field-type.constant';
import { FormInput } from '../form-input.service';

import { IFormInputNumberConfig } from './interfaces/form-input-number-config.interface';

export class FormInputNumber extends FormInput<IFormInputNumberConfig, number> {
  public fieldType: FieldType = FieldType.NUMBER;

  private constructor(config: IFormInputNumberConfig) {
    super(config);
  }

  public static create(config: IFormInputNumberConfig): FormInputNumber {
    return new FormInputNumber(config);
  }
}
