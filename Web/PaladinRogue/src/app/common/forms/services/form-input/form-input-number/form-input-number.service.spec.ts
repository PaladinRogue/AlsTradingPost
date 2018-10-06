import { TestBed } from '@angular/core/testing';

import { FormInputNumber } from './form-input-number.service';

describe('FormInputText.FormInputNumber', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormInputNumber = TestBed.get(FormInputNumber);
    expect(service).toBeTruthy();
  });
});
