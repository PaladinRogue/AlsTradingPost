import { Injectable } from '@angular/core';
import { reduce } from 'lodash';

import { HttpApiService } from '../../../../http';
import { HttpError } from '../../../../http/services/http-error/http-error';
import { ErrorType } from '../../../constants/error-type/error-type.constant';

import { CollectionResource } from '../../collection-resource/collection-resource';
import { ICollectionResource } from '../../collection-resource/collection-resource.interface';
import { DataError } from '../../data-error/data-error';
import { ErrorHandlersProviderService } from '../../error-handlers-provider/error-handlers-provider.service';
import { IErrorHandler } from '../../error-handlers/interfaces/error-handler.interface';
import { ILink } from '../../link/link.interface';
import { Resource } from '../../resource/resource';
import { IResource } from '../../resource/resource.interface';
import { DataService } from '../data.service';

@Injectable()
export class HttpDataService implements DataService {
  private readonly _httpApiService: HttpApiService;
  private readonly _errorHandlersProviderService: ErrorHandlersProviderService;

  constructor(httpApiService: HttpApiService,
              errorHandlersProviderService: ErrorHandlersProviderService) {
    this._httpApiService = httpApiService;
    this._errorHandlersProviderService = errorHandlersProviderService;
  }

  public async get(link: ILink): Promise<IResource> {
    try {
      await this._httpApiService.get(link.getUrl());
    } catch (e) {
      this._handleErrors(e);
    }

    return Resource.create({});
  }

  public async getAll(link: ILink): Promise<ICollectionResource> {
    try {
      await this._httpApiService.get(link.getUrl());
    } catch (e) {
      this._handleErrors(e);
    }

    return CollectionResource.create({});
  }

  public async change(link: ILink, resource: IResource): Promise<IResource> {
    try {
      await this._httpApiService.put(link.getUrl());
    } catch (e) {
      this._handleErrors(e);
    }

    return Resource.create({});
  }

  public async create(link: ILink, resource: IResource): Promise<IResource> {
    try {
      await this._httpApiService.post(link.getUrl());
    } catch (e) {
      this._handleErrors(e);
    }

    return Resource.create({});
  }

  public async delete(link: ILink): Promise<void> {
    try {
      await this._httpApiService.delete(link.getUrl());
    } catch (e) {
      this._handleErrors(e);
    }
  }

  private _handleErrors(httpError: HttpError): DataError {
    return reduce(
      this._errorHandlersProviderService.getForErrorType(ErrorType.UNKNOWN),
      (dataError: DataError, errorHandler: IErrorHandler): DataError => {
        return errorHandler.handle(dataError);
      },
      DataError.create(ErrorType.UNKNOWN));
  }
}
