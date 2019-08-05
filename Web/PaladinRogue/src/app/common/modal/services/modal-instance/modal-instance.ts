import { MatDialogRef } from '@angular/material';

export abstract class ModalInstance<TComponent, TResult> {
  protected readonly _dialog: MatDialogRef<TComponent, TResult>;

  protected constructor(dialog: MatDialogRef<TComponent, TResult>) {
    this._dialog = dialog;
  }

  public close(): void {
    this._dialog.close();
  }
}
