import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { DefaultModalComponent } from '../../../business-components/default-modal/default-modal.component';
import { IDefaultModalConfig } from '../../../business-components/default-modal/interfaces/default-modal-config.interface';
import { DefaultModal } from '../../modal-instance/default-modal/default-modal';

@Injectable({
  providedIn: 'root'
})
export class DefaultModalService {
  private readonly _dialog: MatDialog;

  public constructor(dialog: MatDialog) {
    this._dialog = dialog;
  }

  public open<TContentData>(config: IDefaultModalConfig<TContentData>): Observable<DefaultModal> {
    const modal: MatDialogRef<DefaultModalComponent, void> = this._dialog.open(DefaultModalComponent, {
      data: config,
      disableClose: true
    });

    return modal.afterOpen().pipe(
      map((): DefaultModal => modal.componentInstance.defaultModal)
    );
  }
}
