import { Route } from '@angular/router';

import { NoContentComponent } from '../../common/text/presentation-components/no-content/no-content.component';
import { DatePageComponent } from '../business-components/date-page/date-page.component';
import { DateTimePageComponent } from '../business-components/date-time-page/date-time-page.component';
import { IconPageComponent } from '../business-components/icon-page/icon-page.component';
import { NumberPageComponent } from '../business-components/number-page/number-page.component';
import { PlaygroundLandingPageComponent } from '../business-components/playground-landing-page/playground-landing-page.component';
import { StoragePageComponent } from '../business-components/storage-page/storage-page.component';
import { TimePageComponent } from '../business-components/time-page/time-page.component';

export const PLAYGROUND_ROUTES: Array<Route> = [
  {
    path: 'playground',
    component: PlaygroundLandingPageComponent,
    children: [
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'prefix'
      },
      {
        path: 'home',
        component: NoContentComponent
      },
      {
        path: 'icons',
        component: IconPageComponent
      },
      {
        path: 'dates',
        component: DatePageComponent
      },
      {
        path: 'times',
        component: TimePageComponent
      },
      {
        path: 'dateTimes',
        component: DateTimePageComponent
      },
      {
        path: 'numbers',
        component: NumberPageComponent
      },
      {
        path: 'storage',
        component: StoragePageComponent
      }
    ]
  },
];
