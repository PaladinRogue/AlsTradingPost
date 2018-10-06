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
import { FormInputComponent } from '../../presentation-components/form-input/form-input.component';
import { FormSelectComponent } from '../../presentation-components/form-select/form-select.component';
import { FormField } from '../../services/form-field/form-field.service';
import { FormInput } from '../../services/form-input/form-input.service';
import { FormSelect } from '../../services/form-select/form-select.service';

@Component({
  selector: 'pr-form-field',
  templateUrl: './form-field.component.html',
  styleUrls: ['./form-field.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormFieldComponent implements OnInit, OnDestroy {
  @Input()
  public prFormField: FormField<any, any>;

  @ViewChild('inputContainer', { read: ViewContainerRef })
  public inputContainer: ViewContainerRef;

  public componentRef: ComponentRef<any>;

  private readonly _componentFactoryResolver: ComponentFactoryResolver;

  public constructor(componentFactoryResolver: ComponentFactoryResolver) {
    this._componentFactoryResolver = componentFactoryResolver;
  }

  public ngOnInit(): void {
    let componentFactory: ComponentFactory<any>;

    if (this.prFormField instanceof FormInput) {
      componentFactory = this._componentFactoryResolver.resolveComponentFactory(FormInputComponent);
    } else if (this.prFormField instanceof FormSelect) {
      componentFactory = this._componentFactoryResolver.resolveComponentFactory(FormSelectComponent);
    }

    this.componentRef = this.inputContainer.createComponent(componentFactory);
    this.componentRef.instance.formField = this.prFormField;
  }

  public ngOnDestroy(): void {
    this.componentRef.destroy();
  }
}
