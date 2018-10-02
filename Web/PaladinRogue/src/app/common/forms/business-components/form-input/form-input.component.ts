import {
  ChangeDetectionStrategy,
  Component,
  ComponentFactory,
  ComponentFactoryResolver,
  ComponentRef,
  Input,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef
} from '@angular/core';
import { IFormInputConfig } from '../..';
import { FormInputAbstract } from '../../presentation-components/form-input/form-input';
import { FormInputTextComponent } from '../../presentation-components/form-input/form-input-text/form-input-text.component';
import { FormInput } from '../../services/form-input/form-input.service';

@Component({
  selector: 'pr-form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormInputComponent implements OnInit, OnDestroy {
  @Input()
  public prFormInput: FormInput<IFormInputConfig<any>, any>;

  @ViewChild('inputContainer', { read: ViewContainerRef })
  public inputContainer: ViewContainerRef;

  public componentRef: ComponentRef<FormInputAbstract<IFormInputConfig<any>, any>>;

  private readonly _componentFactoryResolver: ComponentFactoryResolver;

  public constructor(componentFactoryResolver: ComponentFactoryResolver) {
    this._componentFactoryResolver = componentFactoryResolver;
  }

  public ngOnInit(): void {
    const componentFactory: ComponentFactory<FormInputTextComponent> = this._componentFactoryResolver.resolveComponentFactory(FormInputTextComponent);

    this.componentRef = this.inputContainer.createComponent(componentFactory);
    this.componentRef.instance.formInput = this.prFormInput;
  }

  public ngOnDestroy(): void {
    this.componentRef.destroy();
  }
}
