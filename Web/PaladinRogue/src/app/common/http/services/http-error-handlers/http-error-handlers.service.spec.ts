import { TestBed, inject } from '@angular/core/testing';

import { HttpErrorHandlersService } from './http-error-handlers.service';

describe('HttpErrorHandlersService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpErrorHandlersService]
    });
  });

  it('should be created', inject([HttpErrorHandlersService], (service: HttpErrorHandlersService) => {
    expect(service).toBeTruthy();
  }));
});
