import { Injectable } from '@angular/core';

import { IStorage } from '../../interfaces/storage/storage.interface';

@Injectable()
export class SessionStorage implements IStorage {
  private readonly _storage: Storage;

  public constructor(storage: Storage) {
    this._storage = storage;
  }

  public get(key: string): string {
    return this._storage.getItem(key);
  }

  public set(key: string, value: string): void {
    this._storage.setItem(key, value);
  }

  public remove(key: string): void {
    this._storage.removeItem(key);
  }

  public clear(): void {
    this._storage.clear();
  }
}
