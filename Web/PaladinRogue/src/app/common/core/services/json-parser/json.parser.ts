import { Injectable } from '@angular/core';

@Injectable()
export class JsonParser {
  public parse(text: string): any {
    return JSON.parse(text);
  }

  public stringify(jsonValue: any): string {
    return JSON.stringify(jsonValue);
  }
}
