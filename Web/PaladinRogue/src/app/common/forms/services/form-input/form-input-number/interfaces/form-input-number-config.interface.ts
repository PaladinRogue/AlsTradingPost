import { IFormInputConfig } from '../../interfaces/form-input-config.interface';

export interface IFormInputNumberConfig extends IFormInputConfig<number> {
  min?: number;
  max?: number;
}
