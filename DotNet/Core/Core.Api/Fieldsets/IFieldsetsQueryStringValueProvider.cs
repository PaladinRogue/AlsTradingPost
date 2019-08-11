using System;
using System.Collections.Generic;

namespace PaladinRogue.Libray.Core.Api.Fieldsets
{
    public interface IFieldsetsQueryStringValueProvider
    {
        IEnumerable<string> GetFieldsForResourcetype(Type resourceType);
    }
}
