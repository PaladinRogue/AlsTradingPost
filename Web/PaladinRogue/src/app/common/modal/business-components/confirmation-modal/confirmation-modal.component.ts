import { ChangeDetectionStrategy, Component } from '@angular/core';

import { IAction } from '../../../interaction';
import { ITranslate } from '../../../internationalization';
import { ConfirmationModal } from '../../services/modal-instance/confirmation-modal/confirmation-modal';

@Component({
  selector: 'pr-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrls: ['./confirmation-modal.component.scss'],
  providers: [
    ConfirmationModal
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ConfirmationModalComponent {
  public readonly confirmationModal: ConfirmationModal;

  public constructor(confirmationModal: ConfirmationModal) {
    this.confirmationModal = confirmationModal;
  }

  public get message(): ITranslate {
    return this.confirmationModal.getMessage();
  }

  public get declineLabel(): ITranslate {
    return this.confirmationModal.getDeclineLabel() || {
      translateId: 'confirmation.modal.decline'
    };
  }

  public get declineAction(): IAction {
    return {
      action: (): void => {
        this.confirmationModal.decline();
      }
    };
  }

  public get confirmLabel(): ITranslate {
    return this.confirmationModal.getConfirmLabel() || {
      translateId: 'confirmation.modal.confirm'
    };
  }

  public get confirmAction(): IAction {
    return {
      action: (): void => {
        this.confirmationModal.confirm();
      }
    };
  }
}
