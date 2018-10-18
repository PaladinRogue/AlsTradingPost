import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBlankModalConfig } from '../../business-components/blank-modal/interfaces/blank-modal-config.interface';
import { IConfirmationModalConfig } from '../../business-components/confirmation-modal/interfaces/confirmation-modal-config.interface';
import { IDefaultModalConfig } from '../../business-components/default-modal/interfaces/default-modal-config.interface';
import { BlankModal } from '../modal-instance/blank-modal/blank-modal';
import { ConfirmationModal } from '../modal-instance/confirmation-modal/confirmation-modal';
import { DefaultModal } from '../modal-instance/default-modal/default-modal';
import { BlankModalService } from './blank-modal/blank-modal.service';
import { ConfirmationModalService } from './confirmation-modal/confirmation-modal.service';
import { DefaultModalService } from './default-modal/default-modal.service';

@Injectable()
export class ModalService {
  private readonly _defaultModalService: DefaultModalService;
  private readonly _blankModalService: BlankModalService;
  private readonly _confirmationModalService: ConfirmationModalService;

  public constructor(defaultModalService: DefaultModalService,
                     blankModalService: BlankModalService,
                     confirmationModalService: ConfirmationModalService) {
    this._defaultModalService = defaultModalService;
    this._blankModalService = blankModalService;
    this._confirmationModalService = confirmationModalService;
  }

  public openDefault<TContentData>(config: IDefaultModalConfig<TContentData>): Observable<DefaultModal> {
    return this._defaultModalService.open(config);
  }

  public openBlank<TContentData>(config: IBlankModalConfig<TContentData>): Observable<BlankModal> {
    return this._blankModalService.open(config);
  }

  public openConfirmation(config: IConfirmationModalConfig): Observable<ConfirmationModal> {
    return this._confirmationModalService.open(config);
  }
}
