import { HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {
  CldrDateFormat,
  CldrDateTimeFormat,
  CldrTimeFormat,
  DEFAULT_LANGUAGE,
  InternationalizationModule
} from './common/internationalization';
import { DateFormat } from './common/internationalization/services/date-format/date-format';
import { DateTimeFormat } from './common/internationalization/services/date-time-format/date-time-format';
import { CldrNumberFormat } from './common/internationalization/services/number-format/cldr-number-format/cldr-number-format';
import { NumberFormat } from './common/internationalization/services/number-format/number-format';
import { TimeFormat } from './common/internationalization/services/time-format/time-format';
import { DEFAULT_LOCALE } from './common/internationalization/tokens/default-locale.token';
import { DEFAULT_TIMEZONE } from './common/internationalization/tokens/default-timezone.token';
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
    }
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule {
}
