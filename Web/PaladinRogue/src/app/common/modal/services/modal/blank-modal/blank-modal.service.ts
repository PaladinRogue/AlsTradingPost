import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BlankModalComponent } from '../../../business-components/blank-modal/blank-modal.component';
import { IBlankModalConfig } from '../../../business-components/blank-modal/interfaces/blank-modal-config.interface';
import { BlankModal } from '../../modal-instance/blank-modal/blank-modal';

@Injectable({
  providedIn: 'root'
})
export class BlankModalService {
  private readonly _dialog: MatDialog;

  public constructor(dialog: MatDialog) {
    this._dialog = dialog;
  }

  public open<TContentData>(config: IBlankModalConfig<any>): Observable<BlankModal> {
    const modal: MatDialogRef<BlankModalComponent, void> = this._dialog.open(BlankModalComponent, {
      data: config
    });

    return modal.afterOpen().pipe(
      map((): BlankModal => modal.componentInstance.blankModal)
    );
  }
}
