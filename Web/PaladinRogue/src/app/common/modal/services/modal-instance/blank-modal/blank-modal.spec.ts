import { TestBed } from '@angular/core/testing';

import { BlankModal } from './blank-modal';

describe('DefaultModal', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DefaultModalService = TestBed.get(BlankModal);
    expect(service).toBeTruthy();
  });
});
