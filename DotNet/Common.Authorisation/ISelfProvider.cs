using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Authorisation
{
    public interface ISelfProvider
    {
        Task<IDictionary<Type, Guid>> WhoAmIAsync();
    }
}