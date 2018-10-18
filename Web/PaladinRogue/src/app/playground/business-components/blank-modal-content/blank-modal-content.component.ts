import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ModalContentComponent } from '../../../common/modal';

@Component({
  selector: 'pr-blank-modal-content',
  templateUrl: './blank-modal-content.component.html',
  styleUrls: ['./blank-modal-content.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BlankModalContentComponent extends ModalContentComponent<void> {
}
