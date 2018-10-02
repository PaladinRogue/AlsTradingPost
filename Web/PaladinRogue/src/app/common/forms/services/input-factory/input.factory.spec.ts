import { TestBed } from '@angular/core/testing';

import { InputFactory } from './input.factory';

describe('InputFactory', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InputFactory = TestBed.get(InputFactory);
    expect(service).toBeTruthy();
  });
});
