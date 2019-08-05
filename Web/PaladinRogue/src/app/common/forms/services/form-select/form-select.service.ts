import { IOption } from '../../../core';
import { FormField } from '../form-field/form-field.service';
import { IFormSelectConfig } from './interfaces/form-select-config.interface';

export class FormSelect extends FormField<IFormSelectConfig, string> {
  private constructor(config: IFormSelectConfig) {
    super(config);
  }

  public get options(): Array<IOption> {
    return this._fieldConfig.options;
  }

  public static create(config: IFormSelectConfig): FormSelect {
    return new FormSelect(config);
  }
}
