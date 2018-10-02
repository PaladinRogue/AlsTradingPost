import { CommonModule } from '@angular/common';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule as AngularFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { faBars, faCog, faUser } from '@fortawesome/free-solid-svg-icons';
import { FormsModule } from '../common/forms';
import { InteractionModule } from '../common/interaction';
import { InternationalizationModule } from '../common/internationalization';
import { LayoutModule } from '../common/layout';
import { IconRepository, MediaModule } from '../common/media';
import { ModalModule } from '../common/modal';
import { NavigationModule } from '../common/navigation';
import { StorageModule } from '../common/storage';
import { TextModule } from '../common/text';

import { DatePageComponent } from './business-components/date-page/date-page.component';
import { DateTimePageComponent } from './business-components/date-time-page/date-time-page.component';
import { IconPageComponent } from './business-components/icon-page/icon-page.component';
import { LocalePickerComponent } from './business-components/locale-picker/locale-picker.component';
import { MenuComponent } from './business-components/menu/menu.component';
import { NumberPageComponent } from './business-components/number-page/number-page.component';
import { PlaygroundLandingPageComponent } from './business-components/playground-landing-page/playground-landing-page.component';
import { SettingsModalComponent } from './business-components/settings-modal/settings-modal.component';
import { StoragePageComponent } from './business-components/storage-page/storage-page.component';
import { TimePageComponent } from './business-components/time-page/time-page.component';

function initialise_icons(iconRepository: IconRepository): () => void {
  return (): void => {
    iconRepository.addIcon(faBars, faCog, faUser);
  };
}

@NgModule({
  imports: [
    AngularFormsModule,
    InteractionModule,
    FormsModule,
    TextModule,
    InternationalizationModule.forChild(),
    RouterModule,
    MediaModule,
    NavigationModule,
    CommonModule,
    LayoutModule,
    ModalModule,
    StorageModule
  ],
  declarations: [
    PlaygroundLandingPageComponent,
    IconPageComponent,
    MenuComponent,
    DatePageComponent,
    DateTimePageComponent,
    TimePageComponent,
    NumberPageComponent,
    LocalePickerComponent,
    StoragePageComponent,
    SettingsModalComponent
  ],
  entryComponents: [
    SettingsModalComponent
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      deps: [IconRepository],
      useFactory: initialise_icons,
      multi: true
    }
  ]
})
export class PlaygroundModule {
}
