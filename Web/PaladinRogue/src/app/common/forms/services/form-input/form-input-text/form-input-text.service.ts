import { FormInput } from '../form-input.service';

import { IFormInputTextConfig } from './interfaces/form-input-text-config.interface';

export class FormInputText extends FormInput<IFormInputTextConfig, string> {
  private constructor(config: IFormInputTextConfig) {
    super(config);
  }

  public static create(config: IFormInputTextConfig): FormInputText {
    return new FormInputText(config);
  }
}
