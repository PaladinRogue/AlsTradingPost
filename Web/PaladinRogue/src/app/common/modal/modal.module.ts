import { CommonModule } from '@angular/common';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { MAT_DIALOG_DEFAULT_OPTIONS, MatDialogModule } from '@angular/material';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { InteractionModule } from '../interaction';
import { InternationalizationModule } from '../internationalization';
import { IconRepository } from '../media';
import { DefaultModalComponent } from './business-components/default-modal/default-modal.component';
import { ModalInstanceProvider } from './services/modal-instance-provider/modal-instance.provider';
import { ModalService } from './services/modal/modal.service';

function initialise_icons(iconRepository: IconRepository): () => void {
  return (): void => {
    iconRepository.addIcon(faTimes);
  };
}

@NgModule({
  imports: [
    InternationalizationModule.forChild(),
    InteractionModule,
    MatDialogModule,
    CommonModule
  ],
  declarations: [
    DefaultModalComponent
  ],
  entryComponents: [
    DefaultModalComponent
  ],
  providers: [
    ModalService,
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS,
      useValue: {
        hasBackdrop: true,
        autoFocus: true
      }
    },
    ModalInstanceProvider,
    {
      provide: APP_INITIALIZER,
      deps: [IconRepository],
      useFactory: initialise_icons,
      multi: true
    }
  ]
})
export class ModalModule {
}
