using System;
using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Api.Fieldsets
{
    public interface IFieldsetsQueryStringValueProvider
    {
        IEnumerable<string> GetFieldsForResourcetype(Type resourceType);
    }
}
