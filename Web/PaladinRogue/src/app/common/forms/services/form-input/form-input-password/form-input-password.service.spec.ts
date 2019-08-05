import { TestBed } from '@angular/core/testing';

import { FormInputText } from './form-input-password.service';

describe('FormInputText', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormInputText = TestBed.get(FormInputText);
    expect(service).toBeTruthy();
  });
});
