import { HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ENVIRONMENT } from '../environments/environment';

import { AppComponent } from './app.component';
import { APPLICATION_VERSION } from './common/core';
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
import { IconRepository, MaterialIconRepository } from './common/media';
import { HomeModule } from './home/home.module';
import { RoutingModule } from './routing/routing.module';
import { LoginComponent } from './shared/business-components/login/login.component';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    HomeModule,
    RoutingModule,
    BrowserModule,
    SharedModule,
    DataModule,
    BrowserAnimationsModule,
    InternationalizationModule.forRoot({
      backendOptions: {
        loadPath: '/src/assets/locales/{{lng}}/{{ns}}.json'
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
      useFactory: cldrDateFormatFactory
    },
    {
      provide: TimeFormat,
      deps: [HttpClient],
      useFactory: cldrTimeFormatFactory
    },
    {
      provide: DateTimeFormat,
      deps: [HttpClient, DateFormat, TimeFormat],
      useFactory: cldrDateTimeFormatFactory
    },
    {
      provide: NumberFormat,
      deps: [HttpClient],
      useFactory: cldrNumberFormatFactory
    },
    {
      provide: DataService,
      deps: [HttpApiService, ErrorHandlersProvider],
      useFactory: httpDataServiceFactory
    },
    {
      provide: APPLICATION_VERSION,
      useValue: ENVIRONMENT.version
    },
    {
      provide: IconRepository,
      deps: [MatIconRegistry],
      useFactory: materialIconRepositoryFactory
    }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule {
}

export function cldrDateFormatFactory(httpClient: HttpClient): DateFormat {
  return new CldrDateFormat(httpClient, 'node_modules/cldr-dates-modern');
}

export function cldrTimeFormatFactory(httpClient: HttpClient): TimeFormat {
  return new CldrTimeFormat(httpClient, 'node_modules/cldr-dates-modern');
}

export function cldrDateTimeFormatFactory(httpClient: HttpClient,
                                          dateFormat: DateFormat,
                                          timeFormat: TimeFormat): DateTimeFormat {
  return new CldrDateTimeFormat(httpClient, 'node_modules/cldr-dates-modern', dateFormat, timeFormat);
}

export function cldrNumberFormatFactory(httpClient: HttpClient): NumberFormat {
  return new CldrNumberFormat(httpClient, 'node_modules/cldr-numbers-modern');
}

export function httpDataServiceFactory(httpApiService: HttpApiService,
                                       errorHandlersProviderService: ErrorHandlersProvider): DataService {
  return new HttpDataService(httpApiService, errorHandlersProviderService);
}

export function materialIconRepositoryFactory(matIconRegistry: MatIconRegistry): IconRepository {
  return new MaterialIconRepository(matIconRegistry);
}
