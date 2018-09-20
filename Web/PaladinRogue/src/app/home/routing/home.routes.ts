import { Route } from '@angular/router';
import { concat } from 'lodash';

import { ALS_TRADING_POST_ROUTES } from '../../als-trading-post/routing/als-trading-post.routes';
import { PLAYGROUND_ROUTES } from '../../playground/routing/playground.routes';
import { LandingPageComponent } from '../business-components/landing-page/landing-page.component';

export const HOME_ROUTES: Array<Route> = [
  {
    path: '',
    component: LandingPageComponent,
    children: concat(PLAYGROUND_ROUTES, ALS_TRADING_POST_ROUTES)
  },
];
