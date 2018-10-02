import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material';
import { BehaviorSubject, Observable } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { SubscriptionOnDestroy } from '../../../core';
import { IRoute } from '../../interfaces/route.interface';
import { SideNavService } from '../../services/side-nav/side-nav.service';

@Component({
  selector: 'pr-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SideNavComponent extends SubscriptionOnDestroy implements OnInit {
  @Input()
  public prSideNavRoutes: Array<IRoute>;

  @ViewChild(MatSidenav)
  public sideNavComponent: MatSidenav;

  public mobileQuery: MediaQueryList;
  public isOpen$: Observable<boolean>;

  private _isOpenSubject: BehaviorSubject<boolean>;

  private readonly _sideNavService: SideNavService;

  public constructor(changeDetectorRef: ChangeDetectorRef,
                     media: MediaMatcher,
                     sideNavService: SideNavService) {
    super();
    this._sideNavService = sideNavService;

    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this.mobileQuery.addListener(() => changeDetectorRef.detectChanges());
  }

  public ngOnInit(): void {
    this._isOpenSubject = new BehaviorSubject(!this.mobileQuery.matches);
    this.isOpen$ = this._isOpenSubject.asObservable();

    this._sideNavService.onToggle
      .pipe(takeUntil(this._onDestroy))
      .subscribe(() => {
        this._isOpenSubject.next(!this._isOpenSubject.getValue());
      });

    this.sideNavComponent.openedChange
      .pipe(takeUntil(this._onDestroy))
      .subscribe((isOpen: boolean) => {
        this._isOpenSubject.next(isOpen);
      });
  }
}
