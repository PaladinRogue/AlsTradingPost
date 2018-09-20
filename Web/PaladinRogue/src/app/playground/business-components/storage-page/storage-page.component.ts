import { ChangeDetectionStrategy, Component } from '@angular/core';
import { LocalStorage, SessionStorage } from '../../../common/storage';

@Component({
  selector: 'pr-storage',
  templateUrl: './storage-page.component.html',
  styleUrls: ['./storage-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class StoragePageComponent {
  public localData: string;
  public sessionData: string;

  private readonly _localStorage: LocalStorage;
  private readonly _sessionStorage: SessionStorage;

  public constructor(localStorage: LocalStorage,
                     sessionStorage: SessionStorage) {
    this._localStorage = localStorage;
    this._sessionStorage = sessionStorage;
  }

  public getLocalData(): void {
    this.localData = this._localStorage.get('test');
  }

  public saveLocalData(): void {
    this._localStorage.set('test', this.localData);
  }

  public getSessionData(): void {
    this.sessionData = this._sessionStorage.get('test');
  }

  public saveSessionData(): void {
    this._sessionStorage.set('test', this.sessionData);
  }
}
