import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { InternationalizationModule } from '../internationalization';
import { LayoutModule } from '../layout';

import { SideNavComponent } from './presentation-components/side-nav/side-nav.component';

@NgModule({
  imports: [
    InternationalizationModule.forChild(),
    LayoutModule,
    MatListModule,
    RouterModule,
    CommonModule
  ],
  declarations: [
    SideNavComponent
  ],
  exports: [
    SideNavComponent
  ]
})
export class NavigationModule {}
