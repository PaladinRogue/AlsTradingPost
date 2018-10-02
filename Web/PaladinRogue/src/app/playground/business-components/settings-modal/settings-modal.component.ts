import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ModalContentComponent } from '../../../common/modal';

@Component({
  selector: 'pr-settings-modal',
  templateUrl: './settings-modal.component.html',
  styleUrls: ['./settings-modal.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SettingsModalComponent extends ModalContentComponent<void> {
}
