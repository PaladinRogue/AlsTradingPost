import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

import { IconComponent } from './presentation-components/icon/icon.component';

@NgModule({
  imports: [
    MatIconModule
  ],
  declarations: [
    IconComponent
  ],
  exports: [
    IconComponent
  ]
})
export class MediaModule {
}
