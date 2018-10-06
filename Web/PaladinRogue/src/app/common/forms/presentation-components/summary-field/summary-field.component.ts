import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { ITranslate } from '../../../internationalization';

@Component({
  selector: 'pr-summary-field',
  templateUrl: './summary-field.component.html',
  styleUrls: ['./summary-field.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SummaryFieldComponent {
  @Input()
  public prSummaryFieldLabel: ITranslate;
}
