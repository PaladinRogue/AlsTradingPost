import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { InternationalizationModule } from '../common/internationalization';
import { MediaModule } from '../common/media';
import { NavigationModule } from '../common/navigation';
import { TextModule } from '../common/text';

import { DatePageComponent } from './business-components/date-page/date-page.component';
import { DateTimePageComponent } from './business-components/date-time-page/date-time-page.component';
import { IconPageComponent } from './business-components/icon-page/icon-page.component';
import { MenuComponent } from './business-components/menu/menu.component';
import { NumberPageComponent } from './business-components/number-page/number-page.component';
import { PlaygroundLandingPageComponent } from './business-components/playground-landing-page/playground-landing-page.component';
import { TimePageComponent } from './business-components/time-page/time-page.component';
import { LocalePickerComponent } from './business-components/locale-picker/locale-picker.component';
import { StoragePageComponent } from './business-components/storage-page/storage-page.component';

@NgModule({
  imports: [
    FormsModule,
    TextModule,
    InternationalizationModule.forChild(),
    RouterModule,
    MediaModule,
    NavigationModule,
    CommonModule
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
    StoragePageComponent
  ]
})
export class PlaygroundModule {
}
