import { TestBed, inject } from '@angular/core/testing';

import { TimezoneService } from './timezone.service';

describe('TimezoneService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TimezoneService]
    });
  });

  it('should be created', inject([TimezoneService], (service: TimezoneService) => {
    expect(service).toBeTruthy();
  }));
});
