using System.Collections.Generic;
using System.Linq;
using Common.Api.Exceptions;
using Common.Api.Formats.JsonV1.Formats;

namespace Common.Api.Formats.JsonV1.Formatters
{
    public class JsonV1ApplicationErrorFormatter : IApplicationErrorFormatter<FormattedError>
    {
        public FormattedError Format(ApplicationError applicationError)
        {
            return Format(new List<ApplicationError>
            {
                applicationError
            });
        }

        public FormattedError Format(IEnumerable<ApplicationError> applicationErrors)
        {
            return FormattedError.Create(applicationErrors.Select(a => a.FormatError()));
        }
    }
}
