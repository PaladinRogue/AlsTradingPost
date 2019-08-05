import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AlsTradingPostModule } from '../als-trading-post/als-trading-post.module';
import { InternationalizationModule } from '../common/internationalization';
import { PlaygroundModule } from '../playground/playground.module';

import { HOME_ROUTES } from './routing/home.routes';
import { LandingPageComponent } from './business-components/landing-page/landing-page.component';

@NgModule({
  imports: [
    AlsTradingPostModule,
    PlaygroundModule,
    InternationalizationModule.forChild(),
    RouterModule.forChild(HOME_ROUTES)
  ],
  exports: [],
  declarations: [
    LandingPageComponent
  ],
  providers: []
})
export class HomeModule {}
