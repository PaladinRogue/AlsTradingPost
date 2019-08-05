import { TestBed } from '@angular/core/testing';

import { SideNavService } from './side-nav.service';

describe('SideNavService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SideNavService = TestBed.get(SideNavService);
    expect(service).toBeTruthy();
  });
});
