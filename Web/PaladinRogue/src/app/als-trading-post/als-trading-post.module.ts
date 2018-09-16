import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ALS_TRADING_POST_ROUTES } from './routing/als-trading-post.routes';
import { LandingPageComponent } from './business-components/landing-page/landing-page.component';
import { ProfileResolver } from './resolvers/profile/profile.resolver';

@NgModule({
  imports: [
    RouterModule.forChild(ALS_TRADING_POST_ROUTES)
  ],
  exports: [],
  declarations: [
    LandingPageComponent
  ],
  providers: [
    ProfileResolver
  ]
})
export class AlsTradingPostModule {}
