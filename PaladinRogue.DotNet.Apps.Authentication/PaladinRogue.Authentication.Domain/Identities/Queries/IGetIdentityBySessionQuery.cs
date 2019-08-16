using System;
using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Queries
{
    public interface IGetIdentityBySessionQuery
    {
        Task<Identity> RunAsync(Guid sessionId);
    }
}