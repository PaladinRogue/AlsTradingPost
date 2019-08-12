using System.Collections.Generic;
using System.Linq;
using PaladinRogue.Library.Core.Api.Exceptions;
using PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Formatters
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
