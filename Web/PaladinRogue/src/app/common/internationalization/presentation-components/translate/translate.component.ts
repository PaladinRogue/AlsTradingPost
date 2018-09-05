import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

import { ITranslate } from '../../services/translation/interfaces/translate.interface';

@Component({
  selector: 'pr-translate',
  templateUrl: './translate.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TranslateComponent {
  @Input()
  public prTranslate: ITranslate;
}
