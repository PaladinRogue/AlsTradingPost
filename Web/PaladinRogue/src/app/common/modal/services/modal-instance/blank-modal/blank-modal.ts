import { Inject, Type } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { BlankModalComponent } from '../../../business-components/blank-modal/blank-modal.component';
import { IBlankModalConfig } from '../../../business-components/blank-modal/interfaces/blank-modal-config.interface';
import { ModalContentComponent } from '../../../business-components/modal-content/modal-content.component';
import { ModalInstance } from '../modal-instance';

export class BlankModal extends ModalInstance<BlankModalComponent, void> {
  private readonly _config: IBlankModalConfig<any>;

  public constructor(dialog: MatDialogRef<BlankModalComponent, void>,
                     @Inject(MAT_DIALOG_DATA) config: IBlankModalConfig<any>) {
    super(dialog);
    this._config = config;
  }

  public getContentComponent(): Type<ModalContentComponent<any>> {
    return this._config.contentComponent;
  }
}
