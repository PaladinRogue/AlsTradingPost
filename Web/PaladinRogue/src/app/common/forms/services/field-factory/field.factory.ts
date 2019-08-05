import { FieldType } from '../../constants/field-type.constant';
import { FormField } from '../form-field/form-field.service';
import { IFormFieldConfig } from '../form-field/interfaces/form-field-config.interface';
import { FormInputNumber } from '../form-input/form-input-number/form-input-number.service';
import { IFormInputNumberConfig } from '../form-input/form-input-number/interfaces/form-input-number-config.interface';
import { FormInputPassword } from '../form-input/form-input-password/form-input-password.service';
import { IFormInputPasswordConfig } from '../form-input/form-input-password/interfaces/form-input-password-config.interface';
import { FormInputText } from '../form-input/form-input-text/form-input-text.service';
import { IFormInputTextConfig } from '../form-input/form-input-text/interfaces/form-input-text-config.interface';
import { FormSelect } from '../form-select/form-select.service';
import { IFormSelectConfig } from '../form-select/interfaces/form-select-config.interface';

export class FieldFactory {
  public static create(config: IFormInputTextConfig, type: FieldType.TEXT): FormInputText;
  public static create(config: IFormInputNumberConfig, type: FieldType.NUMBER): FormInputNumber;
  public static create(config: IFormInputPasswordConfig, type: FieldType.PASSWORD): FormInputPassword;
  public static create(config: IFormSelectConfig, type: FieldType.SELECT): FormSelect;
  public static create(config: IFormFieldConfig<any>): FormField<IFormFieldConfig<any>, any>;

  public static create(config: IFormFieldConfig<any>, type?: FieldType): FormField<IFormFieldConfig<any>, any> {
    switch (config.type || type) {
      case FieldType.NUMBER:
        return FormInputNumber.create(config);
      case FieldType.TEXT:
        return FormInputText.create(config);
      case FieldType.PASSWORD:
        return FormInputPassword.create(config);
      case FieldType.SELECT:
        return FormSelect.create(config as IFormSelectConfig);
    }
  }
}
