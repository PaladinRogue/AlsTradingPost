import { TestBed, inject } from '@angular/core/testing';

import { HttpRequestFactory } from './http-request-factory.service';

describe('HttpRequestFactory', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpRequestFactory]
    });
  });

  it('should be created', inject([HttpRequestFactory], (service: HttpRequestFactory) => {
    expect(service).toBeTruthy();
  }));
});
