import { TestBed } from '@angular/core/testing';

import { IconFactory } from './icon.factory';

describe('IconFactoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: IconFactory = TestBed.get(IconFactory);
    expect(service).toBeTruthy();
  });
});
