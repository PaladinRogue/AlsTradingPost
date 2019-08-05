import { NgModule } from '@angular/core';
import { HttpModule } from '../http';

import { ErrorHandlersProvider } from './services/error-handlers-provider/error-handlers.provider';
import { ErrorHandlersService } from './services/error-handlers/error-handlers.service';

@NgModule({
  declarations: [],
  exports: [],
  imports: [
    HttpModule
  ],
  providers: [
    ErrorHandlersService,
    ErrorHandlersProvider
  ]
})
export class DataModule {
}
