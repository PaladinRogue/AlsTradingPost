import { InjectionToken } from '@angular/core';

import { IStorage } from '../interfaces/storage/storage.interface';

export const LOCAL_STORAGE: InjectionToken<IStorage> = new InjectionToken('localStorage');
