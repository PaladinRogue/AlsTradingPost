import { TestBed } from '@angular/core/testing';

import { Profile.ResolverService } from './profile.resolver.service';

describe('Profile.ResolverService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: Profile.ResolverService = TestBed.get(Profile.ResolverService);
    expect(service).toBeTruthy();
  });
});
