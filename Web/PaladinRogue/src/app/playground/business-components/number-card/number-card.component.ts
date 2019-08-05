import { Component, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'pr-number-card',
  templateUrl: './number-card.component.html',
  styleUrls: ['./number-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NumberCardComponent {
  public numbers: Array<number> = [
    21314124,
    12.45,
    1.342,
    56.56765
  ];
}
