import { ChangeDetectionStrategy, Component } from '@angular/core';
import { DataService } from './common/data';
import { LocalStorage, SessionStorage } from './common/storage';

@Component({
  selector: 'pr-root',
  templateUrl: './app.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  private readonly _dataService: DataService;

  public constructor(dataService: DataService) {
    this._dataService = dataService;
  }

  public getData(): void {
    this._dataService.get({
      getUrl: (): string => {
        return '/api/traders/5d6733a6-3f09-4871-8064-a9a09b4e2c00';
      }
    });
  }
}
