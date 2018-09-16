import { Route } from '@angular/router';

import { LandingPageComponent } from '../business-components/landing-page/landing-page.component';
import { ProfileResolver } from '../resolvers/profile/profile.resolver';

export const ALS_TRADING_POST_ROUTES: Array<Route> = [
  {
    path: '/alsTradingPost',
    component: LandingPageComponent,
    resolve: {
      profile: ProfileResolver
    }
  },
];
