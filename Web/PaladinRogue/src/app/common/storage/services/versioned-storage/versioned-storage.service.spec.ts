import { TestBed } from '@angular/core/testing';

import { VersionedStorageService } from './versioned-storage.service';

describe('VersionedStorageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VersionedStorageService = TestBed.get(VersionedStorageService);
    expect(service).toBeTruthy();
  });
});
