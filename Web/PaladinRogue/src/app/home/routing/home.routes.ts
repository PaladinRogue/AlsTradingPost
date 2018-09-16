import { Route } from '@angular/router';
import { PLAYGROUND_ROUTES } from '../../playground/routing/playground.routes';

import { LandingPageComponent } from '../business-components/landing-page/landing-page.component';

export const HOME_ROUTES: Array<Route> = [
  {
    path: '',
    component: LandingPageComponent,
    children: PLAYGROUND_ROUTES
  },
];
