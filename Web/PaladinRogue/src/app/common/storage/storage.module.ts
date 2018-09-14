import { NgModule } from '@angular/core';
import { CoreModule, JsonParser } from '../core';

import { IStorage } from './interfaces/storage/storage.interface';
import { JsonParsedStorage } from './services/json-parsed-storage/json-parsed-storage.service';
import { LocalStorage } from './services/local-storage/local-storage.service';
import { SessionStorage } from './services/session-storage/session-storage.service';
import { LOCAL_STORAGE } from './tokens/local-storage.token';
import { SESSION_STORAGE } from './tokens/session-storage.token';

@NgModule({
  declarations: [],
  exports: [],
  imports: [
    CoreModule
  ],
  providers: [
    {
      provide: LOCAL_STORAGE,
      useFactory(): LocalStorage {
        return new LocalStorage(window.localStorage);
      }
    },
    {
      provide: SESSION_STORAGE,
      useFactory(): SessionStorage {
        return new SessionStorage(window.sessionStorage);
      }
    },
    {
      deps: [LOCAL_STORAGE, JsonParser],
      provide: LocalStorage,
      useFactory(localStorage: IStorage, parser: JsonParser): IStorage {
        return new JsonParsedStorage(localStorage, parser);
      }
    },
    {
      deps: [SESSION_STORAGE, JsonParser],
      provide: SessionStorage,
      useFactory(sessionStorage: IStorage, parser: JsonParser): IStorage {
        return new JsonParsedStorage(sessionStorage, parser);
      }
    }
  ]
})
export class StorageModule {
}
