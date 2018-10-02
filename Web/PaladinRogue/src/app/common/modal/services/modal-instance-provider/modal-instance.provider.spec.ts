import { TestBed } from '@angular/core/testing';

import { ModalInstanceProvider } from './modal-instance.provider';

describe('ModalInstanceProvider', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ModalInstanceProvider = TestBed.get(ModalInstanceProvider);
    expect(service).toBeTruthy();
  });
});
