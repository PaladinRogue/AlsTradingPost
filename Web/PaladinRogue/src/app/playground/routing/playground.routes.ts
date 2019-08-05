import { Route } from '@angular/router';

import { UnsavedChangesGuard } from '../../common/forms/guards/unsaved-changes/unsaved-changes.guard';
import { NoContentComponent } from '../../common/text/presentation-components/no-content/no-content.component';
import { FormsPageComponent } from '../business-components/forms-page/forms-page.component';
import { LayoutPageComponent } from '../business-components/layout-page/layout-page.component';
import { LocalizationPageComponent } from '../business-components/localization-page/localization-page.component';
import { MediaPageComponent } from '../business-components/media-page/media-page.component';
import { ModalPageComponent } from '../business-components/modal-page/modal-page.component';
import { PlaygroundLandingPageComponent } from '../business-components/playground-landing-page/playground-landing-page.component';
import { StoragePageComponent } from '../business-components/storage-page/storage-page.component';

export const PLAYGROUND_ROUTE: Route = {
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
      path: 'localization',
      component: LocalizationPageComponent
    },
    {
      path: 'media',
      component: MediaPageComponent
    },
    {
      path: 'layout',
      component: LayoutPageComponent
    },
    {
      path: 'forms',
      component: FormsPageComponent,
      canDeactivate: [ UnsavedChangesGuard ]
    },
    {
      path: 'storage',
      component: StoragePageComponent
    },
    {
      path: 'modal',
      component: ModalPageComponent
    }
  ]
};
