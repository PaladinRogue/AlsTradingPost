using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorisation.Application
{
    public interface ISelfProvider
    {
        Task<IDictionary<Type, Guid>> WhoAmIAsync();
    }
}