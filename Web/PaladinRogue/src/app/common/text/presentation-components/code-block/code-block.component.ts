import { ChangeDetectionStrategy, Component, HostBinding, Input } from '@angular/core';
import { CodeStyle } from './constants/code-style.constant';

@Component({
  selector: 'pr-code-block',
  templateUrl: './code-block.component.html',
  styleUrls: ['./code-block.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CodeBlockComponent {
  @Input()
  public prCodeBlockStyle: CodeStyle;

  @Input()
  public prCodeBlockContent: string;

  @HostBinding('class.PrCodeBlock--typescript')
  public get isTypeScript(): boolean {
    return this.prCodeBlockStyle === CodeStyle.TYPESCRIPT;
  }

  @HostBinding('class.PrCodeBlock--css')
  public get isCss(): boolean {
    return this.prCodeBlockStyle === CodeStyle.CSS;
  }

  @HostBinding('class.PrCodeBlock--html')
  public get isHtml(): boolean {
    return this.prCodeBlockStyle === CodeStyle.HTML;
  }
}
