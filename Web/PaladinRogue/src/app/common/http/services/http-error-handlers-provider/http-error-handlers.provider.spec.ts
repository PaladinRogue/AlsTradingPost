import { TestBed, inject } from '@angular/core/testing';

import { HttpErrorHandlersProvider } from './http-error-handlers.provider';

describe('HttpErrorHandlersProviderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpErrorHandlersProvider]
    });
  });

  it('should be created', inject([HttpErrorHandlersProvider], (service: HttpErrorHandlersProvider) => {
    expect(service).toBeTruthy();
  }));
});
