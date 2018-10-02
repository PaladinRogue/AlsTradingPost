import { Component, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'pr-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoadingSpinnerComponent {
}
