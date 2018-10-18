import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { FieldFactory, FieldType, FormInputNumber, FormInputPassword, FormInputText, FormSelect } from '../../../common/forms';

@Component({
  selector: 'pr-inputs-card',
  templateUrl: './inputs-card.component.html',
  styleUrls: ['./inputs-card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class InputsCardComponent implements OnInit {
  public textInput: FormInputText;
  public numberInput: FormInputNumber;
  public passwordInput: FormInputPassword;
  public optionSelect: FormSelect;

  private text: string;
  private number: number;
  private password: string;
  private option: string;

  public ngOnInit(): void {
    this.textInput = FieldFactory.create({
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
    }, FieldType.TEXT);

    this.passwordInput = FieldFactory.create({
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
    }, FieldType.PASSWORD);

    this.numberInput = FieldFactory.create({
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
    }, FieldType.NUMBER);

    this.optionSelect = FieldFactory.create({
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
  }
}
