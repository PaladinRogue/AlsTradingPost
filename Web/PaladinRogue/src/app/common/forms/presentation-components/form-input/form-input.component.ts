import { ChangeDetectionStrategy, Component } from '@angular/core';

import { TranslateService } from '../../../internationalization';
import { FormInput } from '../../services/form-input/form-input.service';
import { IFormInputConfig } from '../../services/form-input/interfaces/form-input-config.interface';

@Component({
  selector: 'pr-form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormInputComponent<TConfig extends IFormInputConfig<TModelValue>, TModelValue> {
  public formField: FormInput<TConfig, TModelValue>;

  private readonly _translationService: TranslateService;

  public constructor(translationService: TranslateService) {
    this._translationService = translationService;
  }

  public get labelText(): string {
    return this._translationService.fromTranslation(this.formField.label);
  }
}
