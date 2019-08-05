import { TestBed } from '@angular/core/testing';

import { MaterialIconRepository } from './material-icon-repository.service';

describe('MaterialIconRepository', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MaterialIconRepository = TestBed.get(MaterialIconRepository);
    expect(service).toBeTruthy();
  });
});
