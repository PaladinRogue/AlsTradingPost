import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfirmationModalComponent } from '../../../business-components/confirmation-modal/confirmation-modal.component';
import { IConfirmationModalConfig } from '../../../business-components/confirmation-modal/interfaces/confirmation-modal-config.interface';
import { ConfirmationModal } from '../../modal-instance/confirmation-modal/confirmation-modal';

@Injectable({
  providedIn: 'root'
})
export class ConfirmationModalService {
  private readonly _dialog: MatDialog;

  public constructor(dialog: MatDialog) {
    this._dialog = dialog;
  }

  public open<TContentData>(config: IConfirmationModalConfig): Observable<ConfirmationModal> {
    const modal: MatDialogRef<ConfirmationModalComponent, void> = this._dialog.open(ConfirmationModalComponent, {
      data: config,
      disableClose: true
    });

    return modal.afterOpen().pipe(
      map((): ConfirmationModal => modal.componentInstance.confirmationModal)
    );
  }
}
