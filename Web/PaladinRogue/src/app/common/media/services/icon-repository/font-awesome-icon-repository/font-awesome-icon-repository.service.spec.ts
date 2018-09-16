import { TestBed } from '@angular/core/testing';

import { FontAwesomeIconRepository } from './font-awesome-icon-repository.service';

describe('FontAwesomeIconRepositoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FontAwesomeIconRepository = TestBed.get(FontAwesomeIconRepository);
    expect(service).toBeTruthy();
  });
});
