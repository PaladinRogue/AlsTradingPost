import { HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { DataModule, DataService, HttpDataService } from './common/data';
import { ErrorHandlersProvider } from './common/data/services/error-handlers-provider/error-handlers.provider';
import { HttpApiService } from './common/http';
import {
  CldrDateFormat,
  CldrDateTimeFormat,
  CldrNumberFormat,
  CldrTimeFormat,
  DateFormat,
  DateTimeFormat,
  DEFAULT_LANGUAGE,
  DEFAULT_LOCALE,
  DEFAULT_TIMEZONE,
  InternationalizationModule,
  NumberFormat,
  TimeFormat
} from './common/internationalization';
import { StorageModule } from './common/storage';
import { LoginComponent } from './shared/business-components/login/login.component';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    SharedModule,
    FormsModule,
    DataModule,
    StorageModule,
    InternationalizationModule.forRoot({
      backendOptions: {
        loadPath: '/src/locales/{{lng}}/{{ns}}.json'
      }
    })
  ],
  providers: [
    {
      provide: DEFAULT_LANGUAGE,
      useValue: 'en'
    },
    {
      provide: DEFAULT_LOCALE,
      useValue: 'en-GB'
    },
    {
      provide: DEFAULT_TIMEZONE,
      useValue: 'Europe/London'
    },
    {
      provide: DateFormat,
      deps: [HttpClient],
      useFactory: (httpClient: HttpClient): DateFormat => {
        return new CldrDateFormat(httpClient, 'node_modules/cldr-dates-modern');
      }
    },
    {
      provide: TimeFormat,
      deps: [HttpClient],
      useFactory: (httpClient: HttpClient): TimeFormat => {
        return new CldrTimeFormat(httpClient, 'node_modules/cldr-dates-modern');
      }
    },
    {
      provide: DateTimeFormat,
      deps: [HttpClient, DateFormat, TimeFormat],
      useFactory: (httpClient: HttpClient,
                   dateFormat: DateFormat,
                   timeFormat: TimeFormat): DateTimeFormat => {
        return new CldrDateTimeFormat(httpClient, 'node_modules/cldr-dates-modern', dateFormat, timeFormat);
      }
    },
    {
      provide: NumberFormat,
      deps: [HttpClient],
      useFactory: (httpClient: HttpClient): NumberFormat => {
        return new CldrNumberFormat(httpClient, 'node_modules/cldr-numbers-modern');
      }
    },
    {
      provide: DataService,
      deps: [HttpApiService, ErrorHandlersProvider],
      useFactory: (httpApiService: HttpApiService,
                   errorHandlersProviderService: ErrorHandlersProvider): DataService => {
        return new HttpDataService(httpApiService, errorHandlersProviderService);
      }
    }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule {
}
