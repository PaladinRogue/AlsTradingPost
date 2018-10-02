import { FormControl, ValidationErrors, Validators } from '@angular/forms';
import { map } from 'lodash';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { IFormInputConfig } from './interfaces/form-input-config.interface';

import { ITranslate } from '../../../internationalization';
import { FormStatus } from '../../constants/form-status.constant';
import { IValidationError } from '../../presentation-components/form-validation-errors/interfaces/validation-error.interface';

export abstract class FormInput<TConfig extends IFormInputConfig<TModelValue>, TModelValue> {
  public validationErrors$: Observable<Array<IValidationError>>;

  private readonly _formInputConfig: IFormInputConfig<TModelValue>;
  private readonly _formControl: FormControl;
  private readonly _validationErrorsSubject: Subject<Array<IValidationError>>;

  protected constructor(formInputConfig: IFormInputConfig<TModelValue>) {
    this._formInputConfig = formInputConfig;
    this._formControl = new FormControl({ value: this._formInputConfig.getValue(), disabled: this._formInputConfig.isDisabled }, [Validators.required]);

    this._validationErrorsSubject = new BehaviorSubject(this._mapErrors(this._formControl.errors));
    this.validationErrors$ = this._validationErrorsSubject.asObservable();

    this._formControl.statusChanges.subscribe((formStatus: FormStatus) => {
      if (formStatus === FormStatus.INVALID) {
        this._validationErrorsSubject.next(this._mapErrors(this._formControl.errors));
      }
    });

    this._formControl.valueChanges.subscribe((value: TModelValue) => {
      this._formInputConfig.setValue(value);
    });
  }

  public getFormControl(): FormControl {
    return this._formControl;
  }

  public getLabel(): ITranslate {
    return this._formInputConfig.label;
  }

  public hasPlaceholder(): boolean {
    return !!this._formInputConfig.placeholder;
  }

  public getPlaceholder(): ITranslate {
    return this._formInputConfig.placeholder;
  }

  public setValue(value: TModelValue): void {
    this._formControl.setValue(value);
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
