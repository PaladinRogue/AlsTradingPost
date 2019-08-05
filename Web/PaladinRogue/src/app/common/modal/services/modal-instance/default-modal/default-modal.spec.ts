import { TestBed } from '@angular/core/testing';

import { DefaultModal } from './default-modal';

describe('DefaultModal', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DefaultModalService = TestBed.get(DefaultModal);
    expect(service).toBeTruthy();
  });
});
