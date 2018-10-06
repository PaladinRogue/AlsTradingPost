import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { IconComponent } from './presentation-components/icon/icon.component';

@NgModule({
  imports: [
    FontAwesomeModule
  ],
  declarations: [
    IconComponent
  ],
  exports: [
    IconComponent
  ]
})
export class MediaModule { }
