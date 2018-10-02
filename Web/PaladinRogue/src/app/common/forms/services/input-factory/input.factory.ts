import { FormInputText } from '../form-input/form-input-text/form-input-text.service';
import { IFormInputTextConfig } from '../form-input/form-input-text/interfaces/form-input-text-config.interface';

export class InputFactory {
  public static create(config: IFormInputTextConfig): FormInputText {
    return FormInputText.create(config);
  }
}
