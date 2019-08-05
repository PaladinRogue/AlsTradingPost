import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { ITranslate } from '../../../internationalization';

@Component({
  selector: 'pr-submit',
  templateUrl: './submit.component.html',
  styleUrls: ['./submit.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SubmitComponent {
  @Input()
  public prSubmitLabel: ITranslate;

  @Input()
  public prSubmitIsDisabled: boolean;
}
