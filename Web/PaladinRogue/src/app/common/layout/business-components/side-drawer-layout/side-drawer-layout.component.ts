import { ChangeDetectionStrategy, Component, OnInit, ViewChild } from '@angular/core';
import { MatDrawer, MatDrawerToggleResult } from '@angular/material';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { SubscriptionOnDestroy } from '../../../core';
import { IAction } from '../../../interaction';

@Component({
  selector: 'pr-side-drawer-layout',
  templateUrl: './side-drawer-layout.component.html',
  styleUrls: ['./side-drawer-layout.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SideDrawerLayoutComponent extends SubscriptionOnDestroy implements OnInit {
  @ViewChild(MatDrawer, { static: false })
  public drawerComponent: MatDrawer;

  public closeAction: IAction;
  public openAction: IAction;

  public isClosed$: Observable<boolean>;

  private readonly _isClosedSubject: Subject<boolean> = new BehaviorSubject(false);

  public ngOnInit(): void {
    this.isClosed$ = this._isClosedSubject.asObservable();

    this.closeAction = {
      action: (): Promise<MatDrawerToggleResult> => this.drawerComponent.close()
    };

    this.openAction = {
      action: (): Promise<MatDrawerToggleResult> => this.drawerComponent.open()
    };

    this.drawerComponent.openedChange
      .pipe(takeUntil(this._onDestroy))
      .subscribe((isOpen: boolean) => {
        this._isClosedSubject.next(!isOpen);
      });
  }
}
