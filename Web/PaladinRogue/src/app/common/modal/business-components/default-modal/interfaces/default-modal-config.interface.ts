import { Type } from '@angular/core';
import { ModalContentComponent } from '../../../business-components/modal-content/modal-content.component';
import { ITranslate } from '../../../../internationalization';

export interface IDefaultModalConfig<TContentData> {
  title: ITranslate;
  contentComponent: Type<ModalContentComponent<TContentData>>;
  contentComponentData?: TContentData;
}
