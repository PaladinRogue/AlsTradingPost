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
import { IFormFieldConfig } from '../..';
import { FormFieldBaseComponent } from '../../presentation-components/form-field/form-field-base.component';
import { FormInputComponent } from '../../presentation-components/form-input/form-input.component';
import { FormSelectComponent } from '../../presentation-components/form-select/form-select.component';
import { FormField } from '../../services/form-field/form-field.service';
import { FormInput } from '../../services/form-input/form-input.service';
import { FormSelect } from '../..';

@Component({
  selector: 'pr-form-field',
  templateUrl: './form-field.component.html',
  styleUrls: ['./form-field.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormFieldComponent implements OnInit, OnDestroy {
  @Input()
  public prFormField: FormField<IFormFieldConfig<unknown>, unknown>;

  @ViewChild('inputContainer', { read: ViewContainerRef, static: true })
  public inputContainer: ViewContainerRef;

  public componentRef: ComponentRef<FormFieldBaseComponent<FormField<IFormFieldConfig<unknown>, unknown>>>;

  private readonly _componentFactoryResolver: ComponentFactoryResolver;

  public constructor(componentFactoryResolver: ComponentFactoryResolver) {
    this._componentFactoryResolver = componentFactoryResolver;
  }

  public ngOnInit(): void {
    let componentFactory: ComponentFactory<FormFieldBaseComponent<FormField<IFormFieldConfig<unknown>, unknown>>>;

    if (this.prFormField instanceof FormInput) {
      componentFactory = this._componentFactoryResolver.resolveComponentFactory(FormInputComponent);
    } else if (this.prFormField instanceof FormSelect) {
      componentFactory = this._componentFactoryResolver.resolveComponentFactory(FormSelectComponent);
    }

    this.componentRef = this.inputContainer.createComponent<FormFieldBaseComponent<FormField<IFormFieldConfig<unknown>, unknown>>>(componentFactory);
    this.componentRef.instance.formField = this.prFormField;
  }

  public ngOnDestroy(): void {
    this.componentRef.destroy();
  }
}
