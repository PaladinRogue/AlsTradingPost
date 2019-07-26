using System;
using System.Threading.Tasks;
using Authentication.Domain.Identities;
using Authentication.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.Identities
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