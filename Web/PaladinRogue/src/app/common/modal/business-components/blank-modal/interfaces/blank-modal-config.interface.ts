import { Type } from '@angular/core';
import { ModalContentComponent } from '../../../business-components/modal-content/modal-content.component';

export interface IBlankModalConfig<TContentData> {
  contentComponent: Type<ModalContentComponent<TContentData>>;
  contentComponentData?: TContentData;
}
