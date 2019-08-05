import { TestBed } from '@angular/core/testing';

import { FieldFactory } from './field.factory';

describe('InputFactory', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FieldFactory = TestBed.get(FieldFactory);
    expect(service).toBeTruthy();
  });
});
