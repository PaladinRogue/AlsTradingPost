import {
  ChangeDetectionStrategy,
  Component,
  ComponentFactory,
  ComponentFactoryResolver,
  ComponentRef,
  Inject,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef
} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { IAction } from '../../../interaction';

import { ModalContentComponent } from '../../business-components/modal-content/modal-content.component';
import { ModalInstanceProvider } from '../../services/modal-instance-provider/modal-instance.provider';
import { DefaultModal } from '../../services/modal-instance/default-modal/default-modal';
import { IDefaultModalConfig } from './interfaces/default-modal-config.interface';

@Component({
  selector: 'pr-default-modal',
  templateUrl: './default-modal.component.html',
  styleUrls: ['./default-modal.component.scss'],
  providers: [
    {
      provide: DefaultModal,
      deps: [ModalInstanceProvider],
      useFactory: (modalInstanceProvider: ModalInstanceProvider): DefaultModal => modalInstanceProvider.getDefault()
    }
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DefaultModalComponent implements OnInit, OnDestroy {
  @ViewChild('contentContainer', { read: ViewContainerRef })
  public contentContainer: ViewContainerRef;

  public closeAction: IAction;
  public componentRef: ComponentRef<ModalContentComponent<any>>;

  public readonly defaultModalConfig: IDefaultModalConfig<any>;

  private readonly _componentFactoryResolver: ComponentFactoryResolver;
  private readonly _defaultModal: MatDialogRef<DefaultModalComponent>;

  public constructor(defaultModal: MatDialogRef<DefaultModalComponent>,
                     @Inject(MAT_DIALOG_DATA) defaultModalConfig: IDefaultModalConfig<any>,
                     componentFactoryResolver: ComponentFactoryResolver) {
    this._defaultModal = defaultModal;
    this.defaultModalConfig = defaultModalConfig;
    this._componentFactoryResolver = componentFactoryResolver;
  }

  public ngOnInit(): void {
    this.closeAction = {
      action: (): void => {
        this._defaultModal.close();
      }
    };

    const componentFactory: ComponentFactory<ModalContentComponent<any>> =
      this._componentFactoryResolver.resolveComponentFactory(this.defaultModalConfig.contentComponent);

    this.componentRef = this.contentContainer.createComponent(componentFactory);
    this.componentRef.instance.data = this.defaultModalConfig.contentComponentData;
  }

  public ngOnDestroy(): void {
    this.componentRef.destroy();
  }
}
