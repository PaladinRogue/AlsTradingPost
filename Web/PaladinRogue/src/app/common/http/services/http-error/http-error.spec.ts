import { TestBed, inject } from '@angular/core/testing';

import { HttpError } from './http-error';

describe('Error', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpError]
    });
  });

  it('should be created', inject([HttpError], (service: HttpError) => {
    expect(service).toBeTruthy();
  }));
});
