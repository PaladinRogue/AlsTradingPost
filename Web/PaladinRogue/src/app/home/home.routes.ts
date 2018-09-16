import { Route } from '@angular/router';

import { LandingPageComponent } from './presentation-components/landing-page/landing-page.component';
import { ProfileResolver } from './resolvers/profile/profile.resolver';

export const HOME_ROUTES: Array<Route> = [
  {
    path: '',
    component: LandingPageComponent,
    resolve: {
      profile: ProfileResolver
    }
  },
];
