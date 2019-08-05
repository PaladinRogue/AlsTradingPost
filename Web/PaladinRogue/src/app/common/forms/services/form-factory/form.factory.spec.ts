import { TestBed } from '@angular/core/testing';

import { FormFactory } from './form.factory';

describe('FormFactory', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormFactory = TestBed.get(FormFactory);
    expect(service).toBeTruthy();
  });
});
