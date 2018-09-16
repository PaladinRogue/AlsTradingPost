import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { HOME_ROUTES } from './home.routes';
import { LandingPageComponent } from './presentation-components/landing-page/landing-page.component';
import { ProfileResolver } from './resolvers/profile/profile.resolver';

@NgModule({
  imports: [
    RouterModule.forChild(HOME_ROUTES)
  ],
  exports: [],
  declarations: [
    LandingPageComponent
  ],
  providers: [
    ProfileResolver
  ]
})
export class HomeModule {}
