import { TestBed, inject } from '@angular/core/testing';

import { LanguageService } from './language.service';

describe('TranslateService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LanguageService]
    });
  });

  it('should be created', inject([LanguageService], (service: LanguageService) => {
    expect(service).toBeTruthy();
  }));
});
