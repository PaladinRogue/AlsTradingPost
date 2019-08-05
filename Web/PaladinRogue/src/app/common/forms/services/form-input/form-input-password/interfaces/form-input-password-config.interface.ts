import { IFormInputConfig } from '../../interfaces/form-input-config.interface';

export interface IFormInputPasswordConfig extends IFormInputConfig<string> {
  maxLength?: number;
  minLength?: number;
}
