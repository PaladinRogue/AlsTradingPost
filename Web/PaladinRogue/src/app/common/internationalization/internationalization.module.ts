import { CommonModule } from '@angular/common';
import { APP_INITIALIZER, ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import * as i18next from 'i18next';
import { i18n, InitOptions } from 'i18next';
import * as Backend from 'i18next-xhr-backend';

import { DateTimePipe } from './pipes/date-time/date-time.pipe';
import { DatePipe } from './pipes/date/date.pipe';
import { NumberPipe } from './pipes/number/number.pipe';
import { TimePipe } from './pipes/time/time.pipe';
import { TranslatePipe } from './pipes/translate/translate.pipe';
import { DateTimeComponent } from './presentation-components/date-time/date-time.component';
import { DateComponent } from './presentation-components/date/date.component';
import { NumberComponent } from './presentation-components/number/number.component';
import { TimeComponent } from './presentation-components/time/time.component';
import { TranslateComponent } from './presentation-components/translate/translate.component';
import {
  LdmlToMomentDateFormatAdapter
} from './services/date-format-adapters/ldml-moment-date-format-adapter/ldml-to-moment-date-format.adapter';
import { DateFormat } from './services/date-format/date-format';
import { DateTimeFormat } from './services/date-time-format/date-time-format';
import { DateService } from './services/date/date.service';
import { LanguageService } from './services/language/language.service';
import { LocaleService } from './services/locale/locale.service';
import { MomentService } from './services/moment/moment.service';
import { NumberFormat } from './services/number-format/number-format';
import { NumberService } from './services/number/number.service';
import { TimeFormat } from './services/time-format/time-format';
import { TimezoneService } from './services/timezone/timezone.service';
import { TranslateService } from './services/translate/translate.service';
import { I18nextTranslation } from './services/translation/i18next-translation/i18next-translation';
import { i18nToken } from './services/translation/i18next-translation/tokens/i18n.token';
import { i18nConfigToken } from './services/translation/i18next-translation/tokens/i18nConfig.token';
import { i18nXHRToken } from './services/translation/i18next-translation/tokens/i18nXHR.token';
import { ITranslationOptions } from './services/translation/interfaces/translation-options.interface';
import { Translation } from './services/translation/translation';
import { DEFAULT_LANGUAGE } from './tokens/default-language.token';
import { DEFAULT_LOCALE } from './tokens/default-locale.token';
import { DEFAULT_TIMEZONE } from './tokens/default-timezone.token';

function initialise_translations(languageService: LanguageService,
                                 translation: Translation): () => void {
  return (): void => {
    languageService.addLanguageDependant(translation);
  };
}

function initialise_locales(localeService: LocaleService,
                            dateFormat: DateFormat,
                            timeFormat: TimeFormat,
                            dateTimeFormat: DateTimeFormat,
                            numberFormat: NumberFormat,
                            momentService: MomentService): () => void {
  return (): void => {
    localeService.addLocaleDependant(dateFormat);
    localeService.addLocaleDependant(timeFormat);
    localeService.addLocaleDependant(dateTimeFormat);
    localeService.addLocaleDependant(numberFormat);
    localeService.addLocaleDependant(momentService);
  };
}

function initialise_timezones(timezoneService: TimezoneService,
                              momentService: MomentService): () => void {
  return (): void => {
    timezoneService.addTimezoneDependant(momentService);
  };
}

function initialise_language(languageService: LanguageService, defaultLanguageId: string): () => Promise<void> {
  return (): Promise<void> => languageService.setLanguage(defaultLanguageId);
}

function initialise_locale(localeService: LocaleService, defaultLocaleId: string): () => Promise<void> {
  return (): Promise<void> => localeService.setLocale(defaultLocaleId);
}

function initialise_timezone(timezoneService: TimezoneService, defaultTimezoneId: string): () => Promise<void> {
  return (): Promise<void> => timezoneService.setTimezone(defaultTimezoneId);
}

@NgModule({
  declarations: [
    TranslatePipe,
    DatePipe,
    TimePipe,
    DateTimePipe,
    NumberPipe,
    TranslateComponent,
    DateComponent,
    TimeComponent,
    DateTimeComponent,
    NumberComponent
  ],
  exports: [
    NumberComponent,
    TranslateComponent,
    DateComponent,
    TimeComponent,
    DateTimeComponent
  ],
  imports: [
    CommonModule
  ]
})
export class InternationalizationModule {
  constructor(@Optional() @SkipSelf() translateModule: InternationalizationModule) {
    if (translateModule) {
      throw new Error('Use forRoot in the main module only, for other dependencies use the forChild method');
    }
  }

  public static forRoot(translateConfig: ITranslationOptions): ModuleWithProviders {
    return {
      ngModule: InternationalizationModule,
      providers: [
        {
          provide: i18nToken,
          useValue: i18next
        },
        {
          provide: i18nXHRToken,
          useValue: Backend
        },
        {
          provide: i18nConfigToken,
          useValue: {
            fallbackLng: translateConfig.fallbackLanguage,
            backend: translateConfig.backendOptions
          } as InitOptions
        },
        {
          provide: Translation,
          deps: [i18nToken, i18nXHRToken, i18nConfigToken],
          useFactory: (i18nextProvider: i18n, backend: Backend, initOptions: InitOptions): Translation => {
            return new I18nextTranslation(i18nextProvider, backend, initOptions);
          }
        },
        {
          provide: APP_INITIALIZER,
          useFactory: initialise_translations,
          deps: [LanguageService, Translation],
          multi: true
        },
        {
          provide: APP_INITIALIZER,
          useFactory: initialise_locales,
          deps: [LocaleService, DateFormat, TimeFormat, DateTimeFormat, NumberFormat, MomentService],
          multi: true
        },
        {
          provide: APP_INITIALIZER,
          useFactory: initialise_timezones,
          deps: [TimezoneService, MomentService],
          multi: true
        },
        {
          provide: APP_INITIALIZER,
          useFactory: initialise_locale,
          deps: [LocaleService, DEFAULT_LOCALE],
          multi: true
        },
        {
          provide: APP_INITIALIZER,
          useFactory: initialise_language,
          deps: [LanguageService, DEFAULT_LANGUAGE],
          multi: true
        },
        {
          provide: APP_INITIALIZER,
          useFactory: initialise_timezone,
          deps: [TimezoneService, DEFAULT_TIMEZONE],
          multi: true
        },
        LdmlToMomentDateFormatAdapter,
        TranslateService,
        MomentService,
        DateService,
        NumberService,
        LanguageService,
        LocaleService,
        TimezoneService
      ]
    };
  }

  public static forChild(): ModuleWithProviders {
    return {
      ngModule: InternationalizationModule
    };
  }
}
