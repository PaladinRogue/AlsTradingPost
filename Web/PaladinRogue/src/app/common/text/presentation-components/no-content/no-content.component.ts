import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { ITranslate } from '../../../internationalization';

@Component({
  selector: 'pr-no-content',
  templateUrl: './no-content.component.html',
  styleUrls: ['./no-content.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NoContentComponent {
  @Input()
  public prNoContentTranslation: ITranslate;
}
