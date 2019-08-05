import { ChangeDetectionStrategy, Component, Input, TemplateRef, ViewChild } from '@angular/core';
import { ITranslate } from '../../../internationalization';

@Component({
  selector: 'pr-tab',
  templateUrl: './tab.component.html',
  styleUrls: ['./tab.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TabComponent {
  @Input()
  public prTabTitle: ITranslate;

  @ViewChild('content', { read: TemplateRef, static: false })
  public tabViewContent: TemplateRef<void>;
}
