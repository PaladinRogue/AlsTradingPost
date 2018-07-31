using System.Collections.Generic;
using Common.Api.Exceptions;

namespace Common.Api.Formats.JsonV1.Formatters
{
    public class FormattedError : IFormattedError
    {
        public IEnumerable<Error> Errors { get; set; }
    }
}
