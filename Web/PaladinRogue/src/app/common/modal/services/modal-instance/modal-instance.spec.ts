import { TestBed } from '@angular/core/testing';

import { ModalInstance } from './modal-instance';

describe('ModalInstance', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ModalInstance = TestBed.get(ModalInstance);
    expect(service).toBeTruthy();
  });
});
