import { Route } from '@angular/router';

import { AlsLandingPageComponent } from '../business-components/als-landing-page/als-landing-page.component';
import { ProfileResolver } from '../resolvers/profile/profile.resolver';

export const ALS_TRADING_POST_ROUTES: Array<Route> = [
  {
    path: 'alsTradingPost',
    component: AlsLandingPageComponent,
    resolve: {
      profile: ProfileResolver
    }
  },
];
