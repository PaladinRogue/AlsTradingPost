import { TestBed } from '@angular/core/testing';

import { DefaultModalService } from './default-modal.service';

describe('DefaultModalService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DefaultModalService = TestBed.get(DefaultModalService);
    expect(service).toBeTruthy();
  });
});
