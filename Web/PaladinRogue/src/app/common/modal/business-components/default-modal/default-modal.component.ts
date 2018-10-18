import {
  ChangeDetectionStrategy,
  Component,
  ComponentFactory,
  ComponentFactoryResolver,
  ComponentRef,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef
} from '@angular/core';
import { IAction } from '../../../interaction';
import { ITranslate } from '../../../internationalization';

import { ModalContentComponent } from '../../business-components/modal-content/modal-content.component';
import { DefaultModal } from '../../services/modal-instance/default-modal/default-modal';

@Component({
  selector: 'pr-default-modal',
  templateUrl: './default-modal.component.html',
  styleUrls: ['./default-modal.component.scss'],
  providers: [
    DefaultModal
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DefaultModalComponent implements OnInit, OnDestroy {
  @ViewChild('contentContainer', { read: ViewContainerRef })
  public contentContainer: ViewContainerRef;

  public componentRef: ComponentRef<ModalContentComponent<any>>;

  public readonly defaultModal: DefaultModal;
  private readonly _componentFactoryResolver: ComponentFactoryResolver;

  public constructor(defaultModal: DefaultModal,
                     componentFactoryResolver: ComponentFactoryResolver) {
    this.defaultModal = defaultModal;
    this._componentFactoryResolver = componentFactoryResolver;
  }

  public get closeAction(): IAction {
    return {
      action: (): void => {
        this.defaultModal.close();
      }
    };
  }

  public get title(): ITranslate {
    return this.defaultModal.getTitle();
  }

  public ngOnInit(): void {
    const componentFactory: ComponentFactory<ModalContentComponent<any>> =
      this._componentFactoryResolver.resolveComponentFactory(this.defaultModal.getContentComponent());

    this.componentRef = this.contentContainer.createComponent(componentFactory);
  }

  public ngOnDestroy(): void {
    this.componentRef.destroy();
  }
}
