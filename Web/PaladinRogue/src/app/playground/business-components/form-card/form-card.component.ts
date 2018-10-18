import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FieldFactory, FieldType, Form, FormInputNumber, FormInputPassword, FormInputText, FormSelect } from '../../../common/forms';

@Component({
  selector: 'pr-form-card',
  templateUrl: './form-card.component.html',
  styleUrls: ['./form-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormCardComponent implements OnInit {
  public form: Form;

  private text: string;
  private number: number;
  private password: string;
  private option: string;

  public ngOnInit(): void {
    const textInput: FormInputText = FieldFactory.create({
        name: 'text',
        label: {
          translateId: 'inputs.field.text.label'
        },
        isDisabled: false,
        getValue: (): string => {
          return this.text;
        },
        setValue: (value: string): void => {
          this.text = value;
        }
      }, FieldType.TEXT),
      passwordInput: FormInputPassword = FieldFactory.create({
        name: 'password',
        label: {
          translateId: 'inputs.field.password.label'
        },
        isDisabled: false,
        getValue: (): string => {
          return this.password;
        },
        setValue: (value: string): void => {
          this.password = value;
        }
      }, FieldType.PASSWORD),
      numberInput: FormInputNumber = FieldFactory.create({
        name: 'number',
        label: {
          translateId: 'inputs.field.number.label'
        },
        isDisabled: false,
        getValue: (): number => {
          return this.number;
        },
        setValue: (value: number): void => {
          this.number = value;
        }
      }, FieldType.NUMBER),
      optionSelect: FormSelect = FieldFactory.create({
        name: 'option',
        label: {
          translateId: 'inputs.field.option.label'
        },
        isDisabled: false,
        getValue: (): string => {
          return this.option;
        },
        setValue: (value: string): void => {
          this.option = value;
        },
        options: [
          { value: '1', label: 'One' },
          { value: '2', label: 'Two' }
        ]
      }, FieldType.SELECT);

    this.form = Form.create({
      fields: [
        textInput,
        passwordInput,
        numberInput,
        optionSelect
      ],
      onSave: (): Promise<void> => {
        alert('you saved');

        return Promise.resolve();
      }
    });
  }
}
