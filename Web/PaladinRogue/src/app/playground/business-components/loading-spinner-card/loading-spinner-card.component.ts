import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'pr-loading-spinner-card',
  templateUrl: './loading-spinner-card.component.html',
  styleUrls: ['./loading-spinner-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoadingSpinnerCardComponent {
}
