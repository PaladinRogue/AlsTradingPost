import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import * as moment from 'moment';
import { Moment } from 'moment';

import { IOption } from './common/core';
import {
  DEFAULT_LANGUAGE,
  DEFAULT_LOCALE,
  DEFAULT_TIMEZONE,
  LanguageService,
  LocaleService,
  TimezoneService
} from './common/internationalization';

@Component({
  selector: 'pr-root',
  templateUrl: './app.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements OnInit {
  public title: string = 'PaladinRogue';
  public date: Moment = moment();
  public numbers: Array<number> = [
    12.45,
    21314124,
    324234,
    1.342,
    56.56765
  ];
  public language: string;
  public languageOptions: Array<IOption>;
  public locale: string;
  public localeOptions: Array<IOption>;
  public timezone: string;
  public timezoneOptions: Array<IOption>;
  private readonly _localeService: LocaleService;
  private readonly _languageService: LanguageService;
  private readonly _timezoneService: TimezoneService;

  constructor(localeService: LocaleService,
              languageService: LanguageService,
              timezoneService: TimezoneService,
              @Inject(DEFAULT_LANGUAGE) defaultLanguage: string,
              @Inject(DEFAULT_LOCALE) defaultLocale: string,
              @Inject(DEFAULT_TIMEZONE) defaultTimezone: string) {
    this._localeService = localeService;
    this._languageService = languageService;
    this._timezoneService = timezoneService;
    this.language = defaultLanguage;
    this.locale = defaultLocale;
    this.timezone = defaultTimezone;
  }

  public ngOnInit(): void {
    this.languageOptions = [
      { value: 'en', label: 'English' },
      { value: 'de', label: 'German' },
    ];
    this.localeOptions = [
      { value: 'en-GB', label: 'English - UK' },
      { value: 'de', label: 'German' },
    ];
    this.timezoneOptions = [
      { value: 'Europe/London', label: 'English - UK' },
      { value: 'Europe/Berlin', label: 'German' },
    ];
  }

  public setLanguage(): void {
    this._languageService.setLanguage(this.language);
  }

  public setLocale(): void {
    this._localeService.setLocale(this.locale);
  }

  public setTimezone(): void {
    this._timezoneService.setTimezone(this.timezone);
  }
}
