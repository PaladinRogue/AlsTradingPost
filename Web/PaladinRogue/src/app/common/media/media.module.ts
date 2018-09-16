import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { IconComponent } from './presentation-components/icon/icon.component';
import { IconBadgeComponent } from './presentation-components/icon-badge/icon-badge.component';

@NgModule({
  imports: [
    FontAwesomeModule
  ],
  declarations: [
    IconComponent,
    IconBadgeComponent
  ],
  exports: [
    IconComponent,
    IconBadgeComponent
  ]
})
export class MediaModule { }
