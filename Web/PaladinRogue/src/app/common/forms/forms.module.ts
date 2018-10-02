import { CommonModule } from '@angular/common';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule, MatInputModule } from '@angular/material';
import { faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';

import { InternationalizationModule } from '../internationalization';
import { IconRepository, MediaModule } from '../media';

import { FormInputTextComponent } from './presentation-components/form-input/form-input-text/form-input-text.component';
import { FormInputComponent } from './business-components/form-input/form-input.component';
import { FormValidationErrorsComponent } from './presentation-components/form-validation-errors/form-validation-errors.component';

function initialise_icons(iconRepository: IconRepository): () => void {
  return (): void => {
    iconRepository.addIcon(faExclamationTriangle);
  };
}

@NgModule({
  imports: [
    InternationalizationModule.forChild(),
    MediaModule,
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule
  ],
  declarations: [
    FormInputTextComponent,
    FormValidationErrorsComponent,
    FormInputComponent
  ],
  entryComponents: [
    FormInputTextComponent
  ],
  exports: [
    FormInputComponent
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
export class FormsModule {
}
