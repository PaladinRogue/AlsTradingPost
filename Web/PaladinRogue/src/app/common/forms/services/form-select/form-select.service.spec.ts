import { TestBed } from '@angular/core/testing';

import { FormSelectService } from './form-select.service';

describe('FormSelectService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormSelectService = TestBed.get(FormSelectService);
    expect(service).toBeTruthy();
  });
});
