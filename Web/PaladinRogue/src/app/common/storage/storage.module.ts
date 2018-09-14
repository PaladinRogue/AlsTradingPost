import { NgModule } from '@angular/core';
import { APPLICATION_VERSION, CoreModule, JsonParser } from '../core';

import { IStorage } from './interfaces/storage/storage.interface';
import { JsonParsedStorage } from './services/json-parsed-storage/json-parsed-storage.service';
import { LocalStorage } from './services/local-storage/local-storage.service';
import { SessionStorage } from './services/session-storage/session-storage.service';
import { VersionedStorage } from './services/versioned-storage/versioned-storage.service';
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
      deps: [LOCAL_STORAGE, JsonParser, APPLICATION_VERSION],
      provide: LocalStorage,
      useFactory(localStorage: IStorage, parser: JsonParser, version: string): IStorage {
        const jsonParsedStorage: IStorage = new JsonParsedStorage(localStorage, parser);

        return new VersionedStorage(jsonParsedStorage, version);
      }
    },
    {
      deps: [SESSION_STORAGE, JsonParser, APPLICATION_VERSION],
      provide: SessionStorage,
      useFactory(sessionStorage: IStorage, parser: JsonParser, version: string): IStorage {
        const jsonParsedStorage: IStorage = new JsonParsedStorage(sessionStorage, parser);

        return new VersionedStorage(jsonParsedStorage, version);
      }
    }
  ]
})
export class StorageModule {
}
