import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { SecurityModule } from '../security';

import { HttpApiService } from './services/http-api/http-api.service';
import { HttpErrorHandlersProviderService } from './services/http-error-handlers-provider/http-error-handlers-provider.service';
import { HttpErrorHandlersService } from './services/http-error-handlers/http-error-handlers.service';
import { HttpRequestFactory } from './services/http-request-factory/http-request-factory.service';
import { HttpService } from './services/http/http.service';

@NgModule({
  declarations: [],
  exports: [],
  imports: [
    HttpClientModule,
    SecurityModule
  ],
  providers: [
    HttpApiService,
    HttpErrorHandlersService,
    HttpErrorHandlersProviderService,
    HttpRequestFactory,
    HttpService
  ]
})
export class HttpModule {
}
