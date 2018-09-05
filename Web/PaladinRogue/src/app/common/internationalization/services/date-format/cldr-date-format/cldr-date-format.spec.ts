import { TestBed, inject } from '@angular/core/testing';

import { CldrDateFormat } from './cldr-date-format';

describe('CldrDateFormat', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CldrDateFormat]
    });
  });

  it('should be created', inject([CldrDateFormat], (service: CldrDateFormat) => {
    expect(service).toBeTruthy();
  }));
});
