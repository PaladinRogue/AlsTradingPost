import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { InternationalizationModule } from '../common/internationalization';
import { AlsLandingPageComponent } from './business-components/als-landing-page/als-landing-page.component';
import { ProfileResolver } from './resolvers/profile/profile.resolver';

@NgModule({
  imports: [
    InternationalizationModule.forChild(),
    RouterModule
  ],
  declarations: [
    AlsLandingPageComponent
  ],
  providers: [
    ProfileResolver
  ]
})
export class AlsTradingPostModule {
}
