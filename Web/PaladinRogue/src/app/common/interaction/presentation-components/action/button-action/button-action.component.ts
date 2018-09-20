import { ChangeDetectionStrategy, Component } from '@angular/core';

import { ActionComponent } from '../action.component';

@Component({
  selector: 'pr-button-action',
  templateUrl: './button-action.component.html',
  styleUrls: ['./button-action.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ButtonActionComponent extends ActionComponent {
  public constructor() {
    super();
  }
}
