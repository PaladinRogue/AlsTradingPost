import { TestBed, inject } from '@angular/core/testing';

import { CldrNumberFormat } from './cldr-number-format';

describe('CldrNumberFormat', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CldrNumberFormat]
    });
  });

  it('should be created', inject([CldrNumberFormat], (service: CldrNumberFormat) => {
    expect(service).toBeTruthy();
  }));
});
