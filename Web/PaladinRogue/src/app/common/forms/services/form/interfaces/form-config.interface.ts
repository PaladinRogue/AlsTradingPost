import { IFormFieldConfig } from '../../../services/form-field/interfaces/form-field-config.interface';
import { FormField } from '../../form-field/form-field.service';

export interface IFormConfig {
  fields: Array<IFormFieldConfig<any> | FormField<IFormFieldConfig<any>, any>>;

  onSave(): Promise<void>;

  onDelete?(): Promise<void>;
}
