import { APP_INITIALIZER, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule, MatProgressSpinnerModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { IconRepository, MediaModule } from '../media';

import { ButtonActionComponent } from './presentation-components/action/button-action/button-action.component';
import { IconActionComponent } from './presentation-components/action/icon-action/icon-action.component';

import { faAngleLeft, faAngleRight } from '@fortawesome/free-solid-svg-icons';
import { LoadingSpinnerComponent } from './presentation-components/loading-spinner/loading-spinner.component';

function initialise_icons(iconRepository: IconRepository): () => void {
  return (): void => {
    iconRepository.addIcon(faAngleLeft, faAngleRight);
  };
}

@NgModule({
  imports: [
    MediaModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    RouterModule,
    CommonModule
  ],
  declarations: [
    ButtonActionComponent,
    IconActionComponent,
    LoadingSpinnerComponent
  ],
  exports: [
    ButtonActionComponent,
    IconActionComponent,
    LoadingSpinnerComponent
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
