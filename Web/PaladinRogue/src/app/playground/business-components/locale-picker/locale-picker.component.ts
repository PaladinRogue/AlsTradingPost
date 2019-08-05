import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FieldFactory, FieldType, FormSelect } from '../../../common/forms';
import { LanguageService, LocaleService, TimezoneService } from '../../../common/internationalization';

@Component({
  selector: 'pr-locale-picker',
  templateUrl: './locale-picker.component.html',
  styleUrls: ['./locale-picker.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LocalePickerComponent implements OnInit {
  public languageSelect: FormSelect;
  public localeSelect: FormSelect;
  public timezoneSelect: FormSelect;

  private readonly _localeService: LocaleService;
  private readonly _languageService: LanguageService;
  private readonly _timezoneService: TimezoneService;

  public constructor(localeService: LocaleService,
                     languageService: LanguageService,
                     timezoneService: TimezoneService) {
    this._localeService = localeService;
    this._languageService = languageService;
    this._timezoneService = timezoneService;
  }

  public ngOnInit(): void {
    this.languageSelect = FieldFactory.create({
      name: 'language',
      label: {
        translateId: 'locale.language.label'
      },
      getValue: (): string => {
        return this._languageService.getlanguage();
      },
      setValue: (value: string): void => {
        if (value) {
          this._languageService.setLanguage(value);
        }
      },
      options: [
        { value: 'en', label: 'English' },
        { value: 'de', label: 'German' },
      ]
    }, FieldType.SELECT);

    this.localeSelect = FieldFactory.create({
      name: 'locale',
      label: {
        translateId: 'locale.locale.label'
      },
      getValue: (): string => {
        return this._localeService.getLocale();
      },
      setValue: (value: string): void => {
        if (value) {
          this._localeService.setLocale(value);
        }
      },
      options: [
        { value: 'en-GB', label: 'English - UK' },
        { value: 'de', label: 'German' },
      ]
    }, FieldType.SELECT);

    this.timezoneSelect = FieldFactory.create({
      name: 'timezone',
      label: {
        translateId: 'locale.timezone.label'
      },
      getValue: (): string => {
        return this._timezoneService.getTimezone();
      },
      setValue: (value: string): void => {
        if (value) {
          this._timezoneService.setTimezone(value);
        }
      },
      options: [
        { value: 'Europe/London', label: 'English - UK' },
        { value: 'Europe/Berlin', label: 'German' },
      ]
    }, FieldType.SELECT);
  }
}
