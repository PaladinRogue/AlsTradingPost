import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

import { FormInputText, FieldFactory, FieldType } from '../../../common/forms';
import { IAction } from '../../../common/interaction';
import { LocalStorage, SessionStorage } from '../../../common/storage';

@Component({
  selector: 'pr-storage-page',
  templateUrl: './storage-page.component.html',
  styleUrls: ['./storage-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class StoragePageComponent implements OnInit {
  public localData: string;
  public sessionData: string;

  public localDataInput: FormInputText;
  public sessionDataInput: FormInputText;

  public saveLocalDataAction: IAction;
  public getLocalDataAction: IAction;
  public saveSessionDataAction: IAction;
  public getSessionDataAction: IAction;

  private readonly _localStorage: LocalStorage;
  private readonly _sessionStorage: SessionStorage;

  public constructor(localStorage: LocalStorage,
                     sessionStorage: SessionStorage) {
    this._localStorage = localStorage;
    this._sessionStorage = sessionStorage;
  }

  public ngOnInit(): void {
    this.localDataInput = FieldFactory.create({
      label: {
        translateId: 'some.title'
      },
      isDisabled: false,
      getValue: (): string => {
        return this.localData;
      },
      setValue: (value: string): void => {
        this.localData = value;
      }
    }, FieldType.TEXT);

    this.sessionDataInput = FieldFactory.create({
      label: {
        translateId: 'some.title.other'
      },
      isDisabled: false,
      getValue: (): string => {
        return this.sessionData;
      },
      setValue: (value: string): void => {
        this.sessionData = value;
      }
    }, FieldType.TEXT);

    this.saveLocalDataAction = {
      action: (): void => {
        this._localStorage.set('test', this.localData || '');
      }
    };

    this.saveSessionDataAction = {
      action: (): void => {
        this._sessionStorage.set('test', this.sessionData || '');
      }
    };

    this.getLocalDataAction = {
      action: (): void => {
        this.localDataInput.setValue(this._localStorage.get('test'));
      }
    };

    this.getSessionDataAction = {
      action: (): void => {
        this.sessionDataInput.setValue(this._sessionStorage.get('test'));
      }
    };
  }
}
