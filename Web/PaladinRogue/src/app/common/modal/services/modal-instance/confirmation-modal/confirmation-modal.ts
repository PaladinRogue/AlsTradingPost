import { Inject, Injectable } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { isFunction } from 'lodash';
import { Observable, Subject } from 'rxjs';

import { ITranslate } from '../../../../internationalization';
import { ConfirmationModalComponent } from '../../../business-components/confirmation-modal/confirmation-modal.component';
import { IConfirmationModalConfig } from '../../../business-components/confirmation-modal/interfaces/confirmation-modal-config.interface';
import { ModalInstance } from '../modal-instance';
import { ConfirmationResult } from './constants/confirmation-result.constant';

@Injectable()
export class ConfirmationModal extends ModalInstance<ConfirmationModalComponent, void> {
  public result$: Observable<ConfirmationResult>;

  private readonly _config: IConfirmationModalConfig;
  private readonly _resultSubject: Subject<ConfirmationResult>;

  public constructor(dialog: MatDialogRef<ConfirmationModalComponent, void>,
                     @Inject(MAT_DIALOG_DATA) config: IConfirmationModalConfig) {
    super(dialog);
    this._config = config;
    this._resultSubject = new Subject();
    this.result$ = this._resultSubject.asObservable();
  }

  public async confirm(): Promise<void> {
    await this._config.onConfirm();

    this._dialog.close();

    this._resultSubject.next(ConfirmationResult.ACCEPT);
  }

  public async decline(): Promise<void> {
    if (isFunction(this._config.onDecline)) {
      await this._config.onDecline();
    }

    this._dialog.close();

    this._resultSubject.next(ConfirmationResult.DECLINE);
  }

  public getMessage(): ITranslate {
    return this._config.message;
  }

  public getConfirmLabel(): ITranslate {
    return this._config.confirmLabel;
  }

  public getDeclineLabel(): ITranslate {
    return this._config.declineLabel;
  }
}
