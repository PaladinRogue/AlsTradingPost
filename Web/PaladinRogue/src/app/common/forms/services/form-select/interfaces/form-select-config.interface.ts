import { IOption } from '../../../../core';

import { IFormFieldConfig } from '../../form-field/interfaces/form-field-config.interface';

export interface IFormSelectConfig extends IFormFieldConfig<string> {
  options: Array<IOption>;
}
