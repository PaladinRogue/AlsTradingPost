import { TestBed, inject } from '@angular/core/testing';

import { HttpHeaders } from './http-headers';

describe('HttpHeaders', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpHeaders]
    });
  });

  it('should be created', inject([HttpHeaders], (service: HttpHeaders) => {
    expect(service).toBeTruthy();
  }));
});
