import { Component, ChangeDetectionStrategy, Input } from '@angular/core';
import { IRoute } from '../../interfaces/route.interface';

@Component({
  selector: 'pr-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SideNavComponent {
  @Input()
  public prSideNavRoutes: Array<IRoute>;
}
