import { TestBed, inject } from '@angular/core/testing';

import { ErrorHandlersProvider } from './error-handlers.provider';

describe('ErrorHandlersProviderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ErrorHandlersProvider]
    });
  });

  it('should be created', inject([ErrorHandlersProvider], (service: ErrorHandlersProvider) => {
    expect(service).toBeTruthy();
  }));
});
