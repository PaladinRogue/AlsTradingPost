import { HttpClient } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { faHome } from '@fortawesome/free-solid-svg-icons';
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
import { FontAwesomeIconRepository, IconRepository } from './common/media';
import { HomeModule } from './home/home.module';
import { RoutingModule } from './routing/routing.module';
import { LoginComponent } from './shared/business-components/login/login.component';
import { SharedModule } from './shared/shared.module';

function initialise_icons(iconRepository: IconRepository): () => void {
  return (): void => {
    iconRepository.addIcon(faHome);
  };
}

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
    },
    {
      provide: APPLICATION_VERSION,
      useValue: ENVIRONMENT.version
    },
    {
      provide: IconRepository,
      useFactory: (): IconRepository => {
        return new FontAwesomeIconRepository();
      }
    },
    {
      provide: APP_INITIALIZER,
      deps: [IconRepository],
      useFactory: initialise_icons,
      multi: true
    }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule {
}
