import { ChangeDetectionStrategy, Component, ContentChildren } from '@angular/core';
import { TabComponent } from '../tab/tab.component';

@Component({
  selector: 'pr-tab-group',
  templateUrl: './tab-group.component.html',
  styleUrls: ['./tab-group.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TabGroupComponent {
  @ContentChildren(TabComponent)
  public tabs: Array<TabComponent>;
}
