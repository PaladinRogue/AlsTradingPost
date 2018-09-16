import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { InternationalizationModule } from '../common/internationalization';
import { MediaModule } from '../common/media';
import { NavigationModule } from '../common/navigation';

import { LandingPageComponent } from './business-components/landing-page/landing-page.component';
import { IconPageComponent } from './business-components/icon-page/icon-page.component';
import { MenuComponent } from './business-components/menu/menu.component';

@NgModule({
  imports: [
    InternationalizationModule.forChild(),
    RouterModule,
    MediaModule,
    NavigationModule,
    CommonModule
  ],
  declarations: [
    LandingPageComponent,
    IconPageComponent,
    MenuComponent
  ]
})
export class PlaygroundModule {}
