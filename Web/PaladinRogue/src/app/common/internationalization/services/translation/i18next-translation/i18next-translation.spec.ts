import { TestBed, inject } from '@angular/core/testing';

import { I18nextTranslation } from './i18next-translation';

describe('I18nextTranslation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        I18nextTranslation
      ]
    });
  });

  it('should be created', inject([I18nextTranslation], (service: I18nextTranslation) => {
    expect(service).toBeTruthy();
  }));
});
