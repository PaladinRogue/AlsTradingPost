import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'pr-forms-page',
  templateUrl: './forms-page.component.html',
  styleUrls: ['./forms-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormsPageComponent {
}
