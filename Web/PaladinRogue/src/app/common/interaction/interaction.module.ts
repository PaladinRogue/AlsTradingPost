import { APP_INITIALIZER, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { IconRepository, MediaModule } from '../media';

import { ButtonActionComponent } from './presentation-components/action/button-action/button-action.component';
import { IconActionComponent } from './presentation-components/action/icon-action/icon-action.component';

import { faAngleLeft, faAngleRight } from '@fortawesome/free-solid-svg-icons';

function initialise_icons(iconRepository: IconRepository): () => void {
  return (): void => {
    iconRepository.addIcon(faAngleLeft, faAngleRight);
  };
}

@NgModule({
  imports: [
    MediaModule,
    MatButtonModule,
    RouterModule,
    CommonModule
  ],
  declarations: [
    ButtonActionComponent,
    IconActionComponent
  ],
  exports: [
    ButtonActionComponent,
    IconActionComponent
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      deps: [IconRepository],
      useFactory: initialise_icons,
      multi: true
    }
  ]
})
export class InteractionModule {}
