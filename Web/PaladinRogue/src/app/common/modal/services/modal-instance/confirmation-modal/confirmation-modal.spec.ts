import { TestBed } from '@angular/core/testing';

import { ConfirmationModal } from './confirmation-modal';

describe('ConfirmationModal', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConfirmationModal = TestBed.get(ConfirmationModal);
    expect(service).toBeTruthy();
  });
});
