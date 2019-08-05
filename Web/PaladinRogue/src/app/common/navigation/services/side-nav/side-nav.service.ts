import { EventEmitter, Injectable } from '@angular/core';

@Injectable()
export class SideNavService {
  public onToggle: EventEmitter<void> = new EventEmitter();

  public toggle(): void {
    this.onToggle.emit();
  }
}
