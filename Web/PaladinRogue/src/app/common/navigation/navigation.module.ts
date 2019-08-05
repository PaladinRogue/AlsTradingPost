import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatListModule, MatSidenavModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { InternationalizationModule } from '../internationalization';

import { SideNavComponent } from './presentation-components/side-nav/side-nav.component';
import { SideNavService } from './services/side-nav/side-nav.service';

@NgModule({
  imports: [
    InternationalizationModule.forChild(),
    MatSidenavModule,
    MatListModule,
    RouterModule,
    CommonModule
  ],
  declarations: [
    SideNavComponent
  ],
  exports: [
    SideNavComponent
  ],
  providers: [
    SideNavService
  ]
})
export class NavigationModule {}
