import { MatDialogRef } from '@angular/material';
import { DefaultModalComponent } from '../../../business-components/default-modal/default-modal.component';
import { ModalInstance } from '../modal-instance';

export class DefaultModal extends ModalInstance<DefaultModalComponent, void> {
  public static create(dialog: MatDialogRef<DefaultModalComponent, void>): DefaultModal {
    return new DefaultModal(dialog);
  }
}
