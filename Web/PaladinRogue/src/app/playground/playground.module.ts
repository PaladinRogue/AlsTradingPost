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

import { DateCardComponent } from './business-components/date-card/date-card.component';
import { DateTimeCardComponent } from './business-components/date-time-card/date-time-card.component';
import { FormsPageComponent } from './business-components/forms-page/forms-page.component';
import { IconCardComponent } from './business-components/icon-card/icon-card.component';
import { LayoutPageComponent } from './business-components/layout-page/layout-page.component';
import { LocalePickerComponent } from './business-components/locale-picker/locale-picker.component';
import { NumberCardComponent } from './business-components/number-card/number-card.component';
import { PlaygroundLandingPageComponent } from './business-components/playground-landing-page/playground-landing-page.component';
import { SettingsModalComponent } from './business-components/settings-modal/settings-modal.component';
import { StoragePageComponent } from './business-components/storage-page/storage-page.component';
import { TimeCardComponent } from './business-components/time-card/time-card.component';
import { LocalizationPageComponent } from './business-components/localization-page/localization-page.component';
import { MediaPageComponent } from './business-components/media-page/media-page.component';
import { TabsCardComponent } from './business-components/tabs-card/tabs-card.component';
import { LoadingSpinnerCardComponent } from './business-components/loading-spinner-card/loading-spinner-card.component';
import { InputsCardComponent } from './business-components/inputs-card/inputs-card.component';

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
    IconCardComponent,
    DateCardComponent,
    DateTimeCardComponent,
    TimeCardComponent,
    NumberCardComponent,
    LocalePickerComponent,
    StoragePageComponent,
    FormsPageComponent,
    SettingsModalComponent,
    LocalizationPageComponent,
    MediaPageComponent,
    LayoutPageComponent,
    TabsCardComponent,
    LoadingSpinnerCardComponent,
    InputsCardComponent
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
