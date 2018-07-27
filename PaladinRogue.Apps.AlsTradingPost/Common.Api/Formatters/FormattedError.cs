using System.Collections.Generic;
using Common.Api.Exceptions;

namespace Common.Api.Formatters
{
    public class FormattedError : IFormattedError
    {
        public IEnumerable<Error> Errors { get; set; }
    }
}
