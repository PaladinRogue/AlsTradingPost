import { InjectionToken } from '@angular/core';
import * as i18nXHR from 'i18next-xhr-backend';

export const i18nXHRToken: InjectionToken<i18nXHR> = new InjectionToken('i18nXHR');
