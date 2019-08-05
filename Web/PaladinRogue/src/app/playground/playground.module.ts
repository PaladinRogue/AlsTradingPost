import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule as AngularFormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { FormsModule } from '../common/forms';
import { InteractionModule } from '../common/interaction';
import { InternationalizationModule } from '../common/internationalization';
import { LayoutModule } from '../common/layout';
import { MediaModule } from '../common/media';
import { IconFactory } from '../common/media/services/icon-factory/icon.factory';
import { ModalModule } from '../common/modal';
import { NavigationModule } from '../common/navigation';
import { StorageModule } from '../common/storage';
import { TextModule } from '../common/text';
import { BlankModalContentComponent } from './business-components/blank-modal-content/blank-modal-content.component';

import { DateCardComponent } from './business-components/date-card/date-card.component';
import { DateTimeCardComponent } from './business-components/date-time-card/date-time-card.component';
import { FormCardComponent } from './business-components/form-card/form-card.component';
import { FormsPageComponent } from './business-components/forms-page/forms-page.component';
import { IconCardComponent } from './business-components/icon-card/icon-card.component';
import { InputsCardComponent } from './business-components/inputs-card/inputs-card.component';
import { LayoutPageComponent } from './business-components/layout-page/layout-page.component';
import { LoadingSpinnerCardComponent } from './business-components/loading-spinner-card/loading-spinner-card.component';
import { LocalePickerComponent } from './business-components/locale-picker/locale-picker.component';
import { LocalizationPageComponent } from './business-components/localization-page/localization-page.component';
import { MediaPageComponent } from './business-components/media-page/media-page.component';
import { ModalCardComponent } from './business-components/modal-card/modal-card.component';
import { ModalPageComponent } from './business-components/modal-page/modal-page.component';
import { NumberCardComponent } from './business-components/number-card/number-card.component';
import { PlaygroundLandingPageComponent } from './business-components/playground-landing-page/playground-landing-page.component';
import { SettingsModalComponent } from './business-components/settings-modal/settings-modal.component';
import { StoragePageComponent } from './business-components/storage-page/storage-page.component';
import { TabsCardComponent } from './business-components/tabs-card/tabs-card.component';
import { TimeCardComponent } from './business-components/time-card/time-card.component';

@NgModule({
  imports: [
    AngularFormsModule,
    ReactiveFormsModule,
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
    InputsCardComponent,
    FormCardComponent,
    ModalPageComponent,
    ModalCardComponent,
    BlankModalContentComponent
  ],
  entryComponents: [
    SettingsModalComponent,
    BlankModalContentComponent
  ],
  providers: [
    IconFactory
  ]
})
export class PlaygroundModule {
}
