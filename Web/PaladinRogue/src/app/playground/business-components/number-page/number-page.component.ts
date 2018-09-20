import { Component, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'pr-number-page',
  templateUrl: './number-page.component.html',
  styleUrls: ['./number-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NumberPageComponent {
  public numbers: Array<number> = [
    12.45,
    21314124,
    324234,
    1.342,
    56.56765
  ];
}
