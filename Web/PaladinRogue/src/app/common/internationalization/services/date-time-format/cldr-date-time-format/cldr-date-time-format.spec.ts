import { TestBed, inject } from '@angular/core/testing';

import { CldrDateTimeFormat } from './cldr-date-time-format';

describe('CldrDateTimeFormat', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CldrDateTimeFormat]
    });
  });

  it('should be created', inject([CldrDateTimeFormat], (service: CldrDateTimeFormat) => {
    expect(service).toBeTruthy();
  }));
});
