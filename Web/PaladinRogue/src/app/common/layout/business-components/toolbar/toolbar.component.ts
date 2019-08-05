import { ChangeDetectionStrategy, Component, ElementRef } from '@angular/core';

@Component({
  selector: 'pr-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ToolbarComponent {
  protected readonly _hostElement: ElementRef;

  public constructor(hostElement: ElementRef) {
    this._hostElement = hostElement;
  }

  public get colourPalette(): string {
    if (this._hostElement.nativeElement.classList.contains('PrToolbar--primary')) {
      return 'primary';
    } else if (this._hostElement.nativeElement.classList.contains('PrToolbar--accent')) {
      return 'accent';
    } else if (this._hostElement.nativeElement.classList.contains('PrToolbar--warning')) {
      return 'warn';
    }
    return '';
  }
}
