import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'pr-number',
  templateUrl: './number.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NumberComponent {
  @Input()
  public prNumber: number;

  @Input()
  public prNumberPrecision: number;
}
