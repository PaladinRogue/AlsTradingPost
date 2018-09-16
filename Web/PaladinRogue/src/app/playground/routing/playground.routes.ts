import { Route } from '@angular/router';

import { IconPageComponent } from '../business-components/icon-page/icon-page.component';
import { LandingPageComponent } from '../business-components/landing-page/landing-page.component';

export const PLAYGROUND_ROUTES: Array<Route> = [
  {
    path: 'playground',
    component: LandingPageComponent,
    children: [
      {
        path: 'icons',
        component: IconPageComponent,
      }
    ]
  },
];
