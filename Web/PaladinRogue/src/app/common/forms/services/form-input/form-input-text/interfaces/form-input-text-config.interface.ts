import { IFormInputConfig } from '../../interfaces/form-input-config.interface';

export interface IFormInputTextConfig extends IFormInputConfig<string> {
  maxLength?: number;
  minLength?: number;
}
