using System;
using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Queries
{
    public interface IGetIdentityBySessionQuery
    {
        Task<Identity> RunAsync(Guid sessionId);
    }
}