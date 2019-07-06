using System;
using System.Threading.Tasks;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManager.Persistence.Identities
{
    public class GetIdentityBySessionQuery : IGetIdentityBySessionQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public GetIdentityBySessionQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public Task<Identity> RunAsync(Guid sessionId)
        {
            return _applicationManagerDbContext.Identities.SingleOrDefaultAsync(i => i.Session.Id == sessionId);
        }
    }
}