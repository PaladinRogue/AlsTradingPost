import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'pr-modal-page',
  templateUrl: './modal-page.component.html',
  styleUrls: ['./modal-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ModalPageComponent {
}
