import { inject, TestBed } from '@angular/core/testing';

import { LdmlToMomentDateFormatAdapter } from './ldml-to-moment-date-format.adapter';

describe('LdmlMomentDateFormatAdapter', () => {
  let adapter: LdmlToMomentDateFormatAdapter;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LdmlToMomentDateFormatAdapter]
    });
  });

  it('should be created', inject([LdmlToMomentDateFormatAdapter], (ldmlToMomentDateFormatAdapterService: LdmlToMomentDateFormatAdapter) => {
    adapter = ldmlToMomentDateFormatAdapterService;
    expect(ldmlToMomentDateFormatAdapterService).toBeTruthy();
  }));

  describe('when parsing a date format', () => {
    it('[EEEE, d MMMM y] should return [dddd, D MMMM YYYY]', () => {
      expect(adapter.parse('EEEE, d MMMM y')).toEqual('dddd, D MMMM YYYY');
    });

    it('[d MMMM y] should return [D MMMM YYYY]', () => {
      expect(adapter.parse('d MMMM y')).toEqual('D MMMM YYYY');
    });

    it('[d MMM y] should return [D MMM YYYY]', () => {
      expect(adapter.parse('d MMM y')).toEqual('D MMM YYYY');
    });

    it('[dd/MM/y] should return [DD/MM/YYYY]', () => {
      expect(adapter.parse('dd/MM/y')).toEqual('DD/MM/YYYY');
    });
  });

  describe('when parsing a time format', () => {
    it('[HH:mm:ss zzzz] should return [HH:mm:ss zzzz]', () => {
      expect(adapter.parse('HH:mm:ss zzzz')).toEqual('HH:mm:ss zzzz');
    });

    it('[HH:mm:ss z] should return [HH:mm:ss z]', () => {
      expect(adapter.parse('HH:mm:ss z')).toEqual('HH:mm:ss z');
    });

    it('[HH:mm:ss] should return [HH:mm:ss]', () => {
      expect(adapter.parse('HH:mm:ss')).toEqual('HH:mm:ss');
    });

    it('[HH:mm] should return [HH:mm]', () => {
      expect(adapter.parse('HH:mm')).toEqual('HH:mm');
    });

    it('[h:mm a] should return [h:mm a]', () => {
      expect(adapter.parse('h:mm a')).toEqual('h:mm A');
    });

    it('[kk:mm] should return [HH:mm]', () => {
      expect(adapter.parse('kk:mm')).toEqual('HH:mm');
    });
  });
});
