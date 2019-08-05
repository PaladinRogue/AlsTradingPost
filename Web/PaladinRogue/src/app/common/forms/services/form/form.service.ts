import { AbstractControl, FormGroup } from '@angular/forms';
import { isFunction, map, reduce } from 'lodash';
import { Observable, Subject } from 'rxjs';

import { IDict } from '../../../core';
import { FieldFactory } from '../../services/field-factory/field.factory';
import { IFormFieldConfig } from '../../services/form-field/interfaces/form-field-config.interface';
import { FormField } from '../form-field/form-field.service';
import { IFormConfig } from './interfaces/form-config.interface';

export class Form {
  public readonly fieldGroup: FormGroup;
  public readonly onSave$: Observable<void>;

  private readonly _onSaveSubject: Subject<void>;
  private readonly _fields: Array<FormField<IFormFieldConfig<any>, any>>;
  private readonly _formConfig: IFormConfig;

  private constructor(formConfig: IFormConfig) {
    this._formConfig = formConfig;
    this._fields = map(this._formConfig.fields,
      (field: FormField<IFormFieldConfig<any>, any> | IFormFieldConfig<any>): FormField<IFormFieldConfig<any>, any> => {
        if (field instanceof FormField) {
          return field;
        }

        return FieldFactory.create(field);
      });

    this._onSaveSubject = new Subject();
    this.onSave$ = this._onSaveSubject.asObservable();

    this.fieldGroup = new FormGroup(reduce(this._fields, (result: IDict<AbstractControl>, field: FormField<any, any>): IDict<AbstractControl> => {
      result[field.name] = field.formControl;

      return result;
    }, {}));
  }

  public get canDelete(): boolean {
    return isFunction(this._formConfig.onDelete);
  }

  public get canSave(): boolean {
    return this.fieldGroup.dirty && this.fieldGroup.valid;
  }

  public get fields(): Array<FormField<IFormFieldConfig<any>, any>> {
    return this._fields;
  }

  public static create(formConfig: IFormConfig): Form {
    return new Form(formConfig);
  }

  public delete(): void {
    if (this.canDelete) {
      this._formConfig.onDelete();
    }
  }

  public async save(): Promise<void> {
    if (this.canSave) {
      this._onSaveSubject.next(await this._formConfig.onSave());

      this.fieldGroup.markAsPristine();
      this.fieldGroup.markAsUntouched();
    }
  }
}
