import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TranslateService } from '../../../../internationalization';
import { IFormInputTextConfig } from '../../../services/form-input/form-input-text/interfaces/form-input-text-config.interface';

import { FormInputAbstract } from '../form-input';

@Component({
  selector: 'pr-form-input-text',
  templateUrl: './form-input-text.component.html',
  styleUrls: ['./form-input-text.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormInputTextComponent extends FormInputAbstract<IFormInputTextConfig, string> {
  public constructor(translationService: TranslateService) {
    super(translationService);
  }
}
