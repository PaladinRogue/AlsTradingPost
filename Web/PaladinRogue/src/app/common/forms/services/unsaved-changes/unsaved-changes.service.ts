import { Injectable } from '@angular/core';
import { remove, some } from 'lodash';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import { ModalService } from '../../../modal';
import { ConfirmationModal } from '../../../modal/services/modal-instance/confirmation-modal/confirmation-modal';
import { ConfirmationResult } from '../../../modal/services/modal-instance/confirmation-modal/constants/confirmation-result.constant';
import { Form } from '../form/form.service';

@Injectable({
  providedIn: 'root'
})
export class UnsavedChangesService {
  private readonly _forms: Array<Form>;
  private readonly _modalService: ModalService;

  public constructor(modalService: ModalService) {
    this._forms = [];
    this._modalService = modalService;
  }

  public any(): boolean {
    return some(this._forms, (form: Form) => {
      return form.fieldGroup.dirty;
    });
  }

  public prompt(): Observable<boolean> {
    return this._modalService.openConfirmation({
      message: {
        translateId: 'unsavedChanges.modal.message'
      },
      onConfirm: (): Promise<void> => {
        return Promise.resolve();
      }
    }).pipe(
      switchMap((confirmationModal: ConfirmationModal): Observable<boolean> => {
        return confirmationModal.result$.pipe(
          map((confirmationResult: ConfirmationResult): boolean => {
            return confirmationResult === ConfirmationResult.ACCEPT;
          })
        );
      })
    );
  }

  public registerForm(form: Form): void {
    this._forms.push(form);
  }

  public deregisterForm(form: Form): void {
    remove(this._forms, form);
  }
}
