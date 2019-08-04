import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormSelect } from '../..';
import { TranslateService } from '../../../internationalization';
import { IFormInputConfig } from '../../services/form-input/interfaces/form-input-config.interface';
import { FormFieldBaseComponent } from '../form-field/form-field-base.component';

@Component({
  selector: 'pr-form-select',
  templateUrl: './form-select.component.html',
  styleUrls: ['./form-select.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormSelectComponent<TConfig extends IFormInputConfig<TModelValue>, TModelValue>  extends FormFieldBaseComponent<FormSelect> {
  public constructor(translationService: TranslateService) {
    super(translationService);
  }
}
