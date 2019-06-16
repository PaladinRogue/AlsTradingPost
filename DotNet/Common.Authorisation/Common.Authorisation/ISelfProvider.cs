using System;
using System.Collections.Generic;

namespace Common.Authorisation
{
    public interface ISelfProvider
    {
        Guid Id { get; }

        IReadOnlyDictionary<Type, Guid> WhoAmI { get; }
    }
}