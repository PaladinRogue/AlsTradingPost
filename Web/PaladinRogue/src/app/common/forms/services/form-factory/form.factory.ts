import { Form } from '../form/form.service';
import { IFormConfig } from '../form/interfaces/form-config.interface';

export class FormFactory {
  public static create(formConfig: IFormConfig): Form {
    return Form.create(formConfig);
  }
}
