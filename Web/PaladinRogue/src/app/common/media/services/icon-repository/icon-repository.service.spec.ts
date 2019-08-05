import { TestBed } from '@angular/core/testing';

import { IconRepository } from './icon-repository.service';

describe('IconRepository', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: IconRepository = TestBed.get(IconRepository);
    expect(service).toBeTruthy();
  });
});
