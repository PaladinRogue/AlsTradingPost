import { ChangeDetectionStrategy, Component } from '@angular/core';
import { IAction } from '../../../common/interaction';
import { ModalService } from '../../../common/modal';
import { BlankModalContentComponent } from '../blank-modal-content/blank-modal-content.component';

@Component({
  selector: 'pr-modal-card',
  templateUrl: './modal-card.component.html',
  styleUrls: ['./modal-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ModalCardComponent {
  private readonly _modalService: ModalService;

  public constructor(modalService: ModalService) {
    this._modalService = modalService;
  }

  public get openBlankModal(): IAction {
    return {
      action: (): void => {
        this._modalService.openBlank({
          contentComponent: BlankModalContentComponent
        });
      }
    };
  }

  public get openDefaultModal(): IAction {
    return {
      action: (): void => {
        this._modalService.openDefault({
          title: {
            translateId: 'modal.card.default.title'
          },
          contentComponent: BlankModalContentComponent
        });
      }
    };
  }

  public get openConfirmationModal(): IAction {
    return {
      action: (): void => {
        this._modalService.openConfirmation({
          message: {
            translateId: 'modal.card.confirmation.message'
          },
          onConfirm: (): Promise<void> => {
            return Promise.resolve();
          }
        });
      }
    };
  }
}
