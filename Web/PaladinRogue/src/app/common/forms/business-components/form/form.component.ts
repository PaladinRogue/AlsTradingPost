import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Form } from '../../services/form/form.service';

@Component({
  selector: 'pr-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormComponent {
  @Input()
  public prForm: Form;
}
