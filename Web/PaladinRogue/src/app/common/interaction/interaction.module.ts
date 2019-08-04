import { APP_INITIALIZER, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule } from '@angular/router';
import { InternationalizationModule } from '../internationalization';
import { IconRepository, MediaModule } from '../media';
import { IconFactory } from '../media/services/icon-factory/icon.factory';

import { ButtonActionComponent } from './presentation-components/action/button-action/button-action.component';
import { IconActionComponent } from './presentation-components/action/icon-action/icon-action.component';

import { faAngleLeft, faAngleRight } from '@fortawesome/free-solid-svg-icons';
import { LoadingSpinnerComponent } from './presentation-components/loading-spinner/loading-spinner.component';
import { SubmitComponent } from './presentation-components/submit/submit.component';

@NgModule({
  imports: [
    MediaModule,
    MatButtonModule,
    InternationalizationModule.forChild(),
    MatProgressSpinnerModule,
    RouterModule,
    CommonModule
  ],
  declarations: [
    ButtonActionComponent,
    IconActionComponent,
    LoadingSpinnerComponent,
    SubmitComponent
  ],
  exports: [
    ButtonActionComponent,
    IconActionComponent,
    LoadingSpinnerComponent,
    SubmitComponent
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      deps: [IconFactory, IconRepository],
      useFactory: initialiseIcons,
      multi: true
    }
  ]
})
export class InteractionModule {}

export function initialiseIcons(iconFactory: IconFactory, iconRepository: IconRepository): () => void {
  return (): void => {
    iconRepository.addIcon(
      iconFactory.fromFontAwesome(faAngleLeft),
      iconFactory.fromFontAwesome(faAngleRight)
    );
  };
}
