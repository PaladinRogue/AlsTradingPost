import { TestBed, inject } from '@angular/core/testing';

import { Json.ParserService } from './json.parser.service';

describe('Json.ParserService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Json.ParserService]
    });
  });

  it('should be created', inject([Json.ParserService], (service: Json.ParserService) => {
    expect(service).toBeTruthy();
  }));
});
