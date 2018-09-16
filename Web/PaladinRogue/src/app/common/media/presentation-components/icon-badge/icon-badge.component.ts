import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'pr-icon-badge',
  templateUrl: './icon-badge.component.html',
  styleUrls: ['./icon-badge.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class IconBadgeComponent {
  @Input()
  public prIconBadgeIcon: string;
}
