import { Injectable } from '@angular/core';

import { DefaultModal } from '../modal-instance/default-modal/default-modal';
import { ModalInstance } from '../modal-instance/modal-instance';

@Injectable()
export class ModalInstanceProvider {
  private _modalInstance: ModalInstance<any, any>;

  public register(modalInstance: ModalInstance<any, any>): void {
    this._modalInstance = modalInstance;
  }

  public getDefault(): DefaultModal {
    return this._modalInstance as DefaultModal;
  }
}
