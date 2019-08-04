import { Injectable } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { IconDefinition } from '@fortawesome/fontawesome-common-types';
import { IIconDefinition } from '../icon-repository/interfaces/icon-definition.interface';

@Injectable({
  providedIn: 'root'
})
export class IconFactory {
  private readonly _domSanitizer: DomSanitizer;

  public constructor(domSanitizer: DomSanitizer) {
    this._domSanitizer = domSanitizer;
  }

  public fromFontAwesome(iconDefinition: IconDefinition): IIconDefinition {
    const value: SafeHtml = this._domSanitizer.bypassSecurityTrustHtml(
      `<svg aria-hidden="true" focusable="false" role="img"
xmlns="http://www.w3.org/2000/svg" viewBox="0 0 ${iconDefinition.icon[0]} ${iconDefinition.icon[1]}">
<path fill="currentColor" d="${iconDefinition.icon[4]}"></path></svg>`
    );

    return {
      icon: {
        value
      },
      iconName: iconDefinition.iconName
    };
  }
}
