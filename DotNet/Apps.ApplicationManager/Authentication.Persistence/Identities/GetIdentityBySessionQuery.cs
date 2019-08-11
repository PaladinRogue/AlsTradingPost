using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.Identities.Queries;

namespace PaladinRogue.Authentication.Persistence.Identities
{
    public class GetIdentityBySessionQuery : IGetIdentityBySessionQuery
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public GetIdentityBySessionQuery(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public Task<Identity> RunAsync(Guid sessionId)
        {
            return _authenticationDbContext.Identities.SingleOrDefaultAsync(i => i.Session.Id == sessionId);
        }
    }
}