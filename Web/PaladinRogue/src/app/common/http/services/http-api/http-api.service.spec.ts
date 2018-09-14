import { TestBed, inject } from '@angular/core/testing';

import { HttpApiService } from './http-api.service';

describe('HttpApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpApiService]
    });
  });

  it('should be created', inject([HttpApiService], (service: HttpApiService) => {
    expect(service).toBeTruthy();
  }));
});
