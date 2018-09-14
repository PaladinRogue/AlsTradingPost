import { TestBed, inject } from '@angular/core/testing';

import { HttpDataService } from './http-data.service';

describe('HttpDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpDataService]
    });
  });

  it('should be created', inject([HttpDataService], (service: HttpDataService) => {
    expect(service).toBeTruthy();
  }));
});
