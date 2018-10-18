import { CommonModule } from '@angular/common';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule, MatInputModule, MatSelectModule } from '@angular/material';
import { faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';
import { InteractionModule } from '../interaction';

import { InternationalizationModule } from '../internationalization';
import { LayoutModule } from '../layout';
import { IconRepository, MediaModule } from '../media';

import { FormFieldComponent } from './business-components/form-field/form-field.component';
import { UnsavedChangesGuard } from './guards/unsaved-changes/unsaved-changes.guard';
import { FormInputComponent } from './presentation-components/form-input/form-input.component';
import { FormValidationErrorsComponent } from './presentation-components/form-validation-errors/form-validation-errors.component';
import { SummaryFieldComponent } from './presentation-components/summary-field/summary-field.component';
import { FormSelectComponent } from './presentation-components/form-select/form-select.component';
import { FormComponent } from './business-components/form/form.component';
import { FormDirective } from './directives/form.directive';
import { UnsavedChangesService } from './services/unsaved-changes/unsaved-changes.service';

function initialise_icons(iconRepository: IconRepository): () => void {
  return (): void => {
    iconRepository.addIcon(faExclamationTriangle);
  };
}

@NgModule({
  imports: [
    InternationalizationModule.forChild(),
    InteractionModule,
    LayoutModule,
    MediaModule,
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule
  ],
  declarations: [
    FormValidationErrorsComponent,
    FormInputComponent,
    FormFieldComponent,
    SummaryFieldComponent,
    FormSelectComponent,
    FormComponent,
    FormDirective
  ],
  entryComponents: [
    FormInputComponent,
    FormSelectComponent
  ],
  exports: [
    FormFieldComponent,
    SummaryFieldComponent,
    FormComponent,
    FormDirective
  ],
  providers: [
    UnsavedChangesService,
    UnsavedChangesGuard,
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
