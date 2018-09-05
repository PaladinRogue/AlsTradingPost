import { InjectionToken } from '@angular/core';
import { InitOptions } from 'i18next';

export const i18nConfigToken: InjectionToken<InitOptions> = new InjectionToken('i18nConfig');
