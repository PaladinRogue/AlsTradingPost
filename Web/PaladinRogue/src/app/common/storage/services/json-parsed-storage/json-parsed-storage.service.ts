import { Injectable } from '@angular/core';

import { IStorage } from '../../interfaces/storage/storage.interface';
import { JsonParser } from '../../../core/services/json-parser/json.parser';

@Injectable()
export class JsonParsedStorage implements IStorage {
  private readonly _storage: IStorage;
  private readonly _jsonParser: JsonParser;

  public constructor(storage: IStorage, jsonParser: JsonParser) {
    this._storage = storage;
    this._jsonParser = jsonParser;
  }

  public get(key: string): any {
    return this._jsonParser.parse(this._storage.get(key));
  }

  public set(key: string, value: any): void {
    this._storage.set(key, this._jsonParser.stringify(value));
  }

  public clear(): void {
    this._storage.clear();
  }

  public remove(key: string): void {
    this._storage.remove(key);
  }
}
