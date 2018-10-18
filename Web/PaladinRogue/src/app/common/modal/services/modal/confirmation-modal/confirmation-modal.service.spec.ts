import { TestBed } from '@angular/core/testing';

import { ConfirmationModalService } from './confirmation-modal.service';

describe('DefaultModalService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConfirmationModalService = TestBed.get(ConfirmationModalService);
    expect(service).toBeTruthy();
  });
});
