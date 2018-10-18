import { Inject, Injectable, Type } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ITranslate } from '../../../../internationalization';

import { DefaultModalComponent } from '../../../business-components/default-modal/default-modal.component';
import { IDefaultModalConfig } from '../../../business-components/default-modal/interfaces/default-modal-config.interface';
import { ModalContentComponent } from '../../../business-components/modal-content/modal-content.component';
import { ModalInstance } from '../modal-instance';

@Injectable()
export class DefaultModal extends ModalInstance<DefaultModalComponent, void> {
  private readonly _config: IDefaultModalConfig<any>;

  public constructor(dialog: MatDialogRef<DefaultModalComponent, void>,
                     @Inject(MAT_DIALOG_DATA) config: IDefaultModalConfig<any>) {
    super(dialog);
    this._config = config;
  }

  public getTitle(): ITranslate {
    return this._config.title;
  }

  public getContentComponent(): Type<ModalContentComponent<any>> {
    return this._config.contentComponent;
  }
}
