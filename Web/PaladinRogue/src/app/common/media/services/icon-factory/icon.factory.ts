import { Injectable } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class IconFactory {
  private readonly _domSanitizer: DomSanitizer;

  public constructor(domSanitizer: DomSanitizer) {
    this._domSanitizer = domSanitizer;
  }
}
