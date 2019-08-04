import { IStorage } from '../..';

export class VersionedStorage implements IStorage {
  private readonly _storage: IStorage;
  private readonly _version: string;

  public constructor(storage: IStorage,
                     version: string) {
    this._storage = storage;
    this._version = version;
  }

  public get(key: string): unknown {
    return this._storage.get(`${this._version}-${key}`);
  }

  public set(key: string, value: unknown): void {
    this._storage.set(`${this._version}-${key}`, value);
  }

  public remove(key: string): void {
    this._storage.remove(`${this._version}-${key}`);
  }

  public clear(): void {
    this._storage.clear();
  }
}
