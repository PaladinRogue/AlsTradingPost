import { TestBed, inject } from '@angular/core/testing';

import { HttpErrorHandlersProviderService } from './http-error-handlers-provider.service';

describe('HttpErrorHandlersProviderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpErrorHandlersProviderService]
    });
  });

  it('should be created', inject([HttpErrorHandlersProviderService], (service: HttpErrorHandlersProviderService) => {
    expect(service).toBeTruthy();
  }));
});
