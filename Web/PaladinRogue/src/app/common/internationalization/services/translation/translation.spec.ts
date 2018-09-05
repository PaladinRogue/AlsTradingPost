import { TestBed, inject } from '@angular/core/testing';

import { Translation } from './translation';

describe('I18nextTranslation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        Translation
      ]
    });
  });

  it('should be created', inject([Translation], (service: Translation) => {
    expect(service).toBeTruthy();
  }));
});
