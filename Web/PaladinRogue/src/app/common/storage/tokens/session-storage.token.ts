import { InjectionToken } from '@angular/core';

import { IStorage } from '../interfaces/storage/storage.interface';

export const SESSION_STORAGE: InjectionToken<IStorage> = new InjectionToken('sessionStorage');
