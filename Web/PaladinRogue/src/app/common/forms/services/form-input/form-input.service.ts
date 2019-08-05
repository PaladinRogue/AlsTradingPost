import { Validators } from '@angular/forms';
import { get, has, concat } from 'lodash';

import { FieldType } from '../../constants/field-type.constant';
import { IValidator } from '../../interfaces/validator.interface';
import { FormField } from '../form-field/form-field.service';
import { IFormInputConfig } from './interfaces/form-input-config.interface';

export abstract class FormInput<TConfig extends IFormInputConfig<TModelValue>, TModelValue> extends FormField<TConfig, TModelValue> {
  public abstract fieldType: FieldType;

  protected constructor(formInputConfig: TConfig) {
    super(formInputConfig);

    const validators: Array<IValidator> = [];

    if (has(this._fieldConfig, ['min'])) {
      validators.push(Validators.min(this.min));
    }

    if (has(this._fieldConfig, ['max'])) {
      validators.push(Validators.max(this.max));
    }

    if (has(this._fieldConfig, ['minLength'])) {
      validators.push(Validators.minLength(this.minLength));
    }

    if (has(this._fieldConfig, ['maxLength'])) {
      validators.push(Validators.maxLength(this.maxLength));
    }

    this.formControl.setValidators(concat(validators, this.validators));
  }

  public get min(): number {
    return get(this._fieldConfig, ['min'], null);
  }

  public get max(): number {
    return get(this._fieldConfig, ['max'], null);
  }

  public get minLength(): number {
    return get(this._fieldConfig, ['minLength'], null);
  }

  public get maxLength(): number {
    return get(this._fieldConfig, ['maxLength'], null);
  }
}
