import { TestBed } from '@angular/core/testing';

import { FormInputText.ServiceService } from './form-input-text.service.service';

describe('FormInputText.ServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormInputText.ServiceService = TestBed.get(FormInputText.ServiceService);
    expect(service).toBeTruthy();
  });
});
