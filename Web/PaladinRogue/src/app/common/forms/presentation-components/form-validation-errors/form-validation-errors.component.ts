import { Component, ChangeDetectionStrategy, Input } from '@angular/core';

import { IValidationError } from './interfaces/validation-error.interface';

@Component({
  selector: 'pr-form-validation-errors',
  templateUrl: './form-validation-errors.component.html',
  styleUrls: ['./form-validation-errors.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormValidationErrorsComponent {
  @Input()
  public prFormValidationErrors: Array<IValidationError>;
}
