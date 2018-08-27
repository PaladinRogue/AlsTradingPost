using System.Collections.Generic;
using Common.Api.Exceptions;

namespace Common.Api.Formats.JsonV1.Formats
{
    public class FormattedError : IFormattedError
    {
        public IEnumerable<Error> Errors { get; set; }
    }
}
