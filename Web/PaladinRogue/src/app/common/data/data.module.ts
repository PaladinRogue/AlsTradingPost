import { NgModule } from '@angular/core';
import { HttpModule } from '../http';

import { ErrorHandlersProviderService } from './services/error-handlers-provider/error-handlers-provider.service';
import { ErrorHandlersService } from './services/error-handlers/error-handlers.service';

@NgModule({
  declarations: [],
  exports: [],
  imports: [
    HttpModule
  ],
  providers: [
    ErrorHandlersService,
    ErrorHandlersProviderService
  ]
})
export class DataModule {
}
