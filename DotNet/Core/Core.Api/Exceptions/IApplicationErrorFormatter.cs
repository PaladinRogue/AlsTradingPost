using System.Collections.Generic;

namespace PaladinRogue.Libray.Core.Api.Exceptions
{
    public interface IApplicationErrorFormatter<out T> where T: IFormattedError
    {
        T Format(ApplicationError applicationError);

        T Format(IEnumerable<ApplicationError> applicationErrors);
    }
}
