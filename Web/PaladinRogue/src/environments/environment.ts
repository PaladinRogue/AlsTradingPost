// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { IEnvironment } from './environment.interface';

export const ENVIRONMENT: IEnvironment = {
  isProduction: false,
  version: '1.0.0',
  profileEndpoint: '/api/v1/als/profile'
};

/*
 * In development mode, for easier debugging, you can ignore zone related http-error
 * stack frames such as `zone.run`/`zoneDelegate.invokeTask` by importing the
 * below file. Don't forget to comment it out in production mode
 * because it will have a performance impact when errors are thrown
 */
// import 'zone.js/dist/zone-http-error';  // Included with Angular CLI.
