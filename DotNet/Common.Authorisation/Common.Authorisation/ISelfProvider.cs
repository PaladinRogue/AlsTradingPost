using System;
using System.Collections.Generic;

namespace Common.Authorisation
{
    public interface ISelfProvider
    {
        IReadOnlyDictionary<Type, Guid> WhoAmI { get; }
    }
}