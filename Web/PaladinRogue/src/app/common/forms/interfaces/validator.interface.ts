import { AbstractControl } from '@angular/forms';
import { IValidationErrors } from './validation-errors.interface';

export type IValidator = (control: AbstractControl) => IValidationErrors | null;
