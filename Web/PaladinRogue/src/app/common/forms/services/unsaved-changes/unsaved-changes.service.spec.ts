import { TestBed } from '@angular/core/testing';

import { UnsavedChangesService } from './unsaved-changes.service';

describe('UnsavedChangesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UnsavedChangesService = TestBed.get(UnsavedChangesService);
    expect(service).toBeTruthy();
  });
});
