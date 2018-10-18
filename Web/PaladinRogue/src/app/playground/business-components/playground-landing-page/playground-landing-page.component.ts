import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Route } from '@angular/router/src/config';
import { filter, get, map } from 'lodash';
import { UnsavedChangesService } from '../../../common/forms/services/unsaved-changes/unsaved-changes.service';
import { IAction } from '../../../common/interaction';
import { ITranslate } from '../../../common/internationalization';
import { ModalService } from '../../../common/modal';
import { IRoute, SideNavService } from '../../../common/navigation';

import { SettingsModalComponent } from '../settings-modal/settings-modal.component';

@Component({
  selector: 'pr-playground-landing-page',
  templateUrl: './playground-landing-page.component.html',
  styleUrls: ['./playground-landing-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlaygroundLandingPageComponent implements OnInit {
  public routes: Array<IRoute>;
  public toggleSideNavAction: IAction;

  private readonly _router: Router;
  private readonly _sideNavService: SideNavService;
  private readonly _modalService: ModalService;
  private readonly _unsavedChangesService: UnsavedChangesService;

  public constructor(router: Router,
                     sideNavService: SideNavService,
                     modalService: ModalService,
                     unsavedChangesService: UnsavedChangesService) {
    this._router = router;
    this._sideNavService = sideNavService;
    this._modalService = modalService;
    this._unsavedChangesService = unsavedChangesService;
  }

  public get settingsModal(): IAction {
    return {
      action: (): void => {
        this._modalService.openDefault({
          title: {
            translateId: 'settings.modal.title'
          },
          contentComponent: SettingsModalComponent
        });
      }
    };
  }

  public ngOnInit(): void {
    this.toggleSideNavAction = {
      action: (): void => this._sideNavService.toggle()
    };

    this.routes = map(filter(get(this._router.config, ['1', 'children']), (route: Route): boolean => {
      return !!route.path;
    }), (route: Route): IRoute => {
      return {
        label: this._mapRouteToTranslation(route),
        route: route.path,
        routeParams: route.data
      };
    });
  }

  private _mapRouteToTranslation(route: Route): ITranslate {
    return {
      translateId: `playground.routes.${route.path}`
    };
  }
}
