import { TestBed } from '@angular/core/testing';

import { FormInputService } from './form-input.service';

describe('FormInputService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormInputService = TestBed.get(FormInputService);
    expect(service).toBeTruthy();
  });
});
