import { TestBed, inject } from '@angular/core/testing';

import { ErrorHandlersProviderService } from './error-handlers-provider.service';

describe('ErrorHandlersProviderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ErrorHandlersProviderService]
    });
  });

  it('should be created', inject([ErrorHandlersProviderService], (service: ErrorHandlersProviderService) => {
    expect(service).toBeTruthy();
  }));
});
