import { FieldType } from '../../../constants/field-type.constant';
import { FormInput } from '../form-input.service';

import { IFormInputPasswordConfig } from './interfaces/form-input-password-config.interface';

export class FormInputPassword extends FormInput<IFormInputPasswordConfig, string> {
  public fieldType: FieldType = FieldType.PASSWORD;

  private constructor(config: IFormInputPasswordConfig) {
    super(config);
  }

  public static create(config: IFormInputPasswordConfig): FormInputPassword {
    return new FormInputPassword(config);
  }
}
