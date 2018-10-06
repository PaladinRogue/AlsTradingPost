import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'pr-layout-page',
  templateUrl: './layout-page.component.html',
  styleUrls: ['./layout-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LayoutPageComponent {
}
