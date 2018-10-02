import { ChangeDetectionStrategy, Component, ElementRef, Input } from '@angular/core';

import { ActionComponent } from '../action.component';

@Component({
  selector: 'pr-icon-action',
  templateUrl: './icon-action.component.html',
  styleUrls: ['./icon-action.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class IconActionComponent extends ActionComponent {
  @Input()
  public prActionIcon: string;

  public constructor(hostElement: ElementRef) {
    super(hostElement);
  }
}
