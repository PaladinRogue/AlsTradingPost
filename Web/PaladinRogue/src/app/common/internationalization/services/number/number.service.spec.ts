import { inject, TestBed } from '@angular/core/testing';

import { NumberFormat } from '../number-format/number-format';
import { NumberService } from './number.service';

describe('NumberService', () => {
  let numberService: NumberService,
    dependencies: {
      numberFormat: NumberFormat
    };

  beforeEach(() => {
    dependencies = {
      numberFormat: jasmine.createSpyObj('NumberFormat', [
        'getDecimalSeparator',
        'getGroupSeparator',
        'getDecimalFormat'
      ]) as NumberFormat
    };

    (dependencies.numberFormat.getDecimalSeparator as jasmine.Spy).and.returnValue('.');
    (dependencies.numberFormat.getGroupSeparator as jasmine.Spy).and.returnValue(',');
    (dependencies.numberFormat.getDecimalFormat as jasmine.Spy).and.returnValue('#,##0.###');

    TestBed.configureTestingModule({
      providers: [
        { provide: NumberFormat, useValue: dependencies.numberFormat },
        NumberService
      ]
    });
  });

  it('should be created', inject([NumberService], (service: NumberService) => {
    numberService = service;
    expect(service).toBeTruthy();
  }));

  describe('when formatting', () => {
    it('should format [12] to 2 dp as [123.00]', () => {
      expect(numberService.format(12, 2)).toEqual('12.00');
    });

    it('should format [123] to 2 dp as [123.00]', () => {
      expect(numberService.format(123, 2)).toEqual('123.00');
    });

    it('should format [1123] to 2 dp as [1,123.00]', () => {
      expect(numberService.format(1123, 2)).toEqual('1,123.00');
    });

    it('should format [1123] to 0 dp as [1,123.00]', () => {
      expect(numberService.format(1123)).toEqual('1,123');
    });

    it('should format [123123] to 0 dp as [1,123.00]', () => {
      expect(numberService.format(123123)).toEqual('123,123');
    });

    it('should format [123456123] to 0 dp as [1,123.00]', () => {
      expect(numberService.format(23456123)).toEqual('23,456,123');
    });

    it('should format [123456123] to 0 dp as [1,123.00]', () => {
      expect(numberService.format(123456123)).toEqual('123,456,123');
    });
  });
});
