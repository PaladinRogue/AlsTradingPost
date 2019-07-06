using System;
using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetIdentityBySessionQuery
    {
        Task<Identity> RunAsync(Guid sessionId);
    }
}