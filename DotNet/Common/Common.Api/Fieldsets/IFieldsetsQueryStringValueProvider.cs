using System;
using System.Collections.Generic;

namespace Common.Api.Fieldsets
{
    public interface IFieldsetsQueryStringValueProvider
    {
        IEnumerable<string> GetFieldsForResourcetype(Type resourceType);
    }
}
