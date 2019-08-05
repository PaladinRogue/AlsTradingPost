import { TestBed } from '@angular/core/testing';

import { FormInput } from './form-input.service';

describe('FormInput', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormInput<any, any> = TestBed.get(FormInput);
    expect(service).toBeTruthy();
  });
});
