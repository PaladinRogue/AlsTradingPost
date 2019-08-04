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

import { ModalContentComponent } from '../../business-components/modal-content/modal-content.component';
import { BlankModal } from '../../services/modal-instance/blank-modal/blank-modal';

@Component({
  selector: 'pr-blank-modal',
  templateUrl: './blank-modal.component.html',
  styleUrls: ['./blank-modal.component.scss'],
  providers: [
    BlankModal
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BlankModalComponent implements OnInit, OnDestroy {
  @ViewChild('contentContainer', { read: ViewContainerRef, static: true })
  public contentContainer: ViewContainerRef;

  public componentRef: ComponentRef<ModalContentComponent<unknown>>;

  public readonly blankModal: BlankModal;
  private readonly _componentFactoryResolver: ComponentFactoryResolver;

  public constructor(blankModal: BlankModal,
                     componentFactoryResolver: ComponentFactoryResolver) {
    this.blankModal = blankModal;
    this._componentFactoryResolver = componentFactoryResolver;
  }

  public ngOnInit(): void {
    const componentFactory: ComponentFactory<ModalContentComponent<unknown>> =
      this._componentFactoryResolver.resolveComponentFactory(this.blankModal.getContentComponent());

    this.componentRef = this.contentContainer.createComponent(componentFactory);
  }

  public ngOnDestroy(): void {
    this.componentRef.destroy();
  }
}
