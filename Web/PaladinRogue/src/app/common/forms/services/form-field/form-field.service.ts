import { FormControl, ValidationErrors } from '@angular/forms';
import { get, has, map } from 'lodash';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { ITranslate } from '../../../internationalization';
import { FormStatus } from '../../constants/form-status.constant';
import { IValidator } from '../../interfaces/validator.interface';
import { IValidationError } from '../../presentation-components/form-validation-errors/interfaces/validation-error.interface';
import { IFormFieldConfig } from './interfaces/form-field-config.interface';

export abstract class FormField<TConfig extends IFormFieldConfig<TModelValue>, TModelValue> {
  public validationErrors$: Observable<Array<IValidationError>>;

  protected readonly _fieldConfig: TConfig;
  private readonly _formControl: FormControl;
  private readonly _validationErrorsSubject: Subject<Array<IValidationError>>;

  protected constructor(fieldConfig: TConfig) {
    this._fieldConfig = fieldConfig;
    this._formControl = new FormControl({
        value: this._fieldConfig.getValue() || '',
        disabled: this.isDisabled
      },
      this.validators);

    this._validationErrorsSubject = new BehaviorSubject(this._mapErrors(this._formControl.errors));
    this.validationErrors$ = this._validationErrorsSubject.asObservable();

    this._formControl.statusChanges.subscribe((formStatus: FormStatus) => {
      if (formStatus === FormStatus.INVALID) {
        this._validationErrorsSubject.next(this._mapErrors(this._formControl.errors));
      }
    });

    this._formControl.valueChanges.subscribe((value: TModelValue) => {
      this._fieldConfig.setValue(value);
    });
  }

  public get name(): string {
    return get(this._fieldConfig, 'name');
  }

  public get label(): ITranslate {
    return get(this._fieldConfig, 'label');
  }

  public get hasPlaceholder(): boolean {
    return has(this._fieldConfig, 'placeholder');
  }

  public get placeholder(): ITranslate {
    return get(this._fieldConfig, 'placeholder', null);
  }

  public get formControl(): FormControl {
    return this._formControl;
  }

  public get validators(): Array<IValidator> {
    return get(this._fieldConfig, 'validators', []);
  }

  public get value(): TModelValue {
    return this._formControl.value;
  }

  public setValue(value: TModelValue): void {
    this._formControl.setValue(value);
  }

  public get isRequired(): boolean {
    return get(this._fieldConfig, 'isRequired', null);
  }

  public get isDisabled(): boolean {
    return get(this._fieldConfig, 'isDisabled', null);
  }

  private _mapErrors(validationErrors: ValidationErrors): Array<IValidationError> {
    return map(validationErrors, (value: any, key: string): IValidationError => {
      return {
        message: {
          translateId: `form.validation.${ key }`,
          translateValues: {
            value
          }
        }
      };
    });
  }

}
