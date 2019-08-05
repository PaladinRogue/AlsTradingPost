import { TestBed } from '@angular/core/testing';

import { FormInputText } from './form-input-text.service';

describe('FormInputText', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormInputText = TestBed.get(FormInputText);
    expect(service).toBeTruthy();
  });
});
