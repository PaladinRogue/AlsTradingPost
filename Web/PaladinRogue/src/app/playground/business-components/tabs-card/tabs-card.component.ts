import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'pr-tabs-card',
  templateUrl: './tabs-card.component.html',
  styleUrls: ['./tabs-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TabsCardComponent {
}
