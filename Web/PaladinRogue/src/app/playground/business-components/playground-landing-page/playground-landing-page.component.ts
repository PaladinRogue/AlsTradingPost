import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Route } from '@angular/router/src/config';
import { map, get, filter } from 'lodash';
import { ITranslate } from '../../../common/internationalization';

import { IRoute } from '../../../common/navigation';

@Component({
  selector: 'pr-playground-landing-page',
  templateUrl: './playground-landing-page.component.html',
  styleUrls: ['./playground-landing-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlaygroundLandingPageComponent implements OnInit {
  public routes: Array<IRoute>;

  private readonly _router: Router;

  public constructor(router: Router) {
    this._router = router;
  }

  public ngOnInit(): void {
    this.routes = map(filter(get(this._router.config, ['0', 'children', '0', 'children']), (route: Route): boolean => {
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
