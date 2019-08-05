import { OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

export abstract class SubscriptionOnDestroy implements OnDestroy {
  protected readonly _onDestroy: Subject<void> = new Subject<void>();

  public ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
