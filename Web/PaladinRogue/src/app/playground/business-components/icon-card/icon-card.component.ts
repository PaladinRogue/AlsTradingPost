import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'pr-icon-card',
  templateUrl: './icon-card.component.html',
  styleUrls: ['./icon-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class IconCardComponent {}
