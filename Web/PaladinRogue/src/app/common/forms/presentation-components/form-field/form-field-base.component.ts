import { TranslateService } from '../../../internationalization';
import { FormField } from '../../services/form-field/form-field.service';
import { IFormFieldConfig } from '../../services/form-field/interfaces/form-field-config.interface';

export abstract class FormFieldBaseComponent<TFormField extends FormField<IFormFieldConfig<unknown>, unknown>> {
  public formField: TFormField;

  protected readonly _translationService: TranslateService;

  protected constructor(translationService: TranslateService) {
    this._translationService = translationService;
  }

  public get labelText(): string {
    return this._translationService.fromTranslation(this.formField.label);
  }
}
