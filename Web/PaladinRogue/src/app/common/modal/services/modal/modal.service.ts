import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { BehaviorSubject, noop, Observable, Subject } from 'rxjs';
import { DefaultModalComponent } from '../../business-components/default-modal/default-modal.component';
import { IDefaultModalConfig } from '../../business-components/default-modal/interfaces/default-modal-config.interface';
import { ModalInstanceProvider } from '../modal-instance-provider/modal-instance.provider';
import { DefaultModal } from '../modal-instance/default-modal/default-modal';
import { ModalInstance } from '../modal-instance/modal-instance';

@Injectable()
export class ModalService {
  private readonly _dialog: MatDialog;
  private readonly _modalInstanceProvider: ModalInstanceProvider;

  private _modalSubject: Subject<ModalInstance<any, any>>;

  public constructor(dialog: MatDialog,
                     modalInstanceProvider: ModalInstanceProvider) {
    this._dialog = dialog;
    this._modalInstanceProvider = modalInstanceProvider;
  }

  public openDefault<TContentData>(config: IDefaultModalConfig<TContentData>): Observable<DefaultModal> {
    const modal: MatDialogRef<DefaultModalComponent, void> = this._dialog.open(DefaultModalComponent, {
      data: config
    });

    modal.afterOpen().subscribe(noop, noop, () => {
      this._modalSubject.complete();
    });

    const defaultModal: DefaultModal = DefaultModal.create(modal);

    this._modalInstanceProvider.register(defaultModal);

    this._modalSubject = new BehaviorSubject(defaultModal);

    return this._modalSubject.asObservable();
  }
}
