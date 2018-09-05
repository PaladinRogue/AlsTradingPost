import { TestBed, inject } from '@angular/core/testing';

import { CldrTimeFormat } from './cldr-time-format';

describe('CldrTimeFormat', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CldrTimeFormat]
    });
  });

  it('should be created', inject([CldrTimeFormat], (service: CldrTimeFormat) => {
    expect(service).toBeTruthy();
  }));
});
