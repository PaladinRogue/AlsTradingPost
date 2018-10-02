import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { ITranslate } from '../../../internationalization';

@Component({
  selector: 'pr-card-header',
  templateUrl: './card-header.component.html',
  styleUrls: ['./card-header.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CardHeaderComponent {
  @Input()
  public prCardHeaderTitle: ITranslate;

  @Input()
  public prCardHeaderSubTitle: ITranslate;
}
