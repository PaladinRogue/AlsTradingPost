import { TestBed, inject } from '@angular/core/testing';

import { ErrorHandlersService } from './error-handlers.service';

describe('ErrorHandlersService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ErrorHandlersService]
    });
  });

  it('should be created', inject([ErrorHandlersService], (service: ErrorHandlersService) => {
    expect(service).toBeTruthy();
  }));
});
