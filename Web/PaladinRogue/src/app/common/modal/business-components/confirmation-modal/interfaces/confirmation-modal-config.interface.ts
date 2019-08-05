import { ITranslate } from '../../../../internationalization';

export interface IConfirmationModalConfig {
  message: ITranslate;
  confirmLabel?: ITranslate;
  declineLabel?: ITranslate;

  onConfirm(): Promise<void>;

  onDecline?(): Promise<void>;
}
