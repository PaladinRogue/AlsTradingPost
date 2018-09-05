import './string-extensions';

describe('StringExtensions', () => {
  describe('when formatting a string with no replacements', () => {
    it('should return the original string', () => {
      expect('some string'.format()).toEqual('some string');
    });
  });

  describe('when formatting a string with a replacement', () => {
    it('should return the string containing the replacement', () => {
      expect('some {0}'.format('string')).toEqual('some string');
    });
  });

  describe('when formatting a string with replacements', () => {
    it('should return the string containing the replacements', () => {
      expect('some {0} {1}'.format('string', 1)).toEqual('some string 1');
    });
  });

  describe('when formatting a string with duplicate replacements', () => {
    it('should return the string with the replacement multiple times', () => {
      expect('some {0} {0}'.format('string')).toEqual('some string string');
    });
  });
});
