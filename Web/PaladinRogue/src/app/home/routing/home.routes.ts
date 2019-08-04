import { Route } from '@angular/router';

import { ALS_TRADING_POST_ROUTES } from '../../als-trading-post/routing/als-trading-post.routes';
import { PLAYGROUND_ROUTE } from '../../playground/routing/playground.routes';
import { LandingPageComponent } from '../business-components/landing-page/landing-page.component';

export const HOME_ROUTES: Array<Route> = [
  {
    path: '',
    component: LandingPageComponent,
    children: ALS_TRADING_POST_ROUTES
  },
  PLAYGROUND_ROUTE
];
