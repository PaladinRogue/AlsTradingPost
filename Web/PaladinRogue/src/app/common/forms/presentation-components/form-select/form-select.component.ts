import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormSelect } from '../..';
import { TranslateService } from '../../../internationalization';
import { IFormInputConfig } from '../../services/form-input/interfaces/form-input-config.interface';

@Component({
  selector: 'pr-form-select',
  templateUrl: './form-select.component.html',
  styleUrls: ['./form-select.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormSelectComponent<TConfig extends IFormInputConfig<TModelValue>, TModelValue> {
  public formField: FormSelect;

  private readonly _translationService: TranslateService;

  public constructor(translationService: TranslateService) {
    this._translationService = translationService;
  }

  public get labelText(): string {
    return this._translationService.fromTranslation(this.formField.label);
  }
}
