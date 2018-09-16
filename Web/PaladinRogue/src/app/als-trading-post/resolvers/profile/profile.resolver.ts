import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';

import { ENVIRONMENT } from '../../../../environments/environment';
import { DataService, IResource } from '../../../common/data/index';
import { Method } from '../../../common/data/constants/method/method.constant';
import { Link } from '../../../common/data/services/link/link';

@Injectable()
export class ProfileResolver implements Resolve<IResource> {
  private readonly _dataService: DataService;

  public constructor(dataService: DataService) {
    this._dataService = dataService;
  }

  public resolve(): Promise<IResource> {
    return this._dataService.get(Link.create(ENVIRONMENT.profileEndpoint, Method.GET));
  }
}
