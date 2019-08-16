using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Self
{
    public interface ISelfProvider
    {
        Task<IDictionary<Type, Guid>> WhoAmIAsync();
    }
}