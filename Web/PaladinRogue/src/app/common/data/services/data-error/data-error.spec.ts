import { TestBed, inject } from '@angular/core/testing';

import { DataError } from './data-error';

describe('DataError', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DataError]
    });
  });

  it('should be created', inject([DataError], (service: DataError) => {
    expect(service).toBeTruthy();
  }));
});
