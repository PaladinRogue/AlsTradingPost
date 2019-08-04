import { ChangeDetectionStrategy, Component } from '@angular/core';

import { TranslateService } from '../../../internationalization';
import { FormInput } from '../../services/form-input/form-input.service';
import { IFormInputConfig } from '../../services/form-input/interfaces/form-input-config.interface';
import { FormFieldBaseComponent } from '../form-field/form-field-base.component';

@Component({
  selector: 'pr-form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormInputComponent<TConfig extends IFormInputConfig<TModelValue>, TModelValue> extends FormFieldBaseComponent<FormInput<TConfig, TModelValue>> {
  public constructor(translationService: TranslateService) {
    super(translationService);
  }
}
