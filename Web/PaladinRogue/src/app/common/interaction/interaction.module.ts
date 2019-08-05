import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule } from '@angular/router';
import { InternationalizationModule } from '../internationalization';
import { MediaModule } from '../media';

import { ButtonActionComponent } from './presentation-components/action/button-action/button-action.component';
import { IconActionComponent } from './presentation-components/action/icon-action/icon-action.component';
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
  providers: []
})
export class InteractionModule {
}
