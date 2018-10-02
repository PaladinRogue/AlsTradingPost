import { IFormInputConfig } from '../../services/form-input/interfaces/form-input-config.interface';
import { TranslateService } from '../../../internationalization';
import { FormInput } from '../../services/form-input/form-input.service';

export abstract class FormInputAbstract<TConfig extends IFormInputConfig<TModelValue>, TModelValue> {
  public formInput: FormInput<TConfig, TModelValue>;

  private readonly _translationService: TranslateService;

  protected constructor(translationService: TranslateService) {
    this._translationService = translationService;
  }

  public get labelText(): string {
    return this._translationService.fromTranslation(this.formInput.getLabel());
  }
}
