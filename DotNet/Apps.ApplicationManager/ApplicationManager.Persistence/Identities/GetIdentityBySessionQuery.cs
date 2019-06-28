using System;
using System.Linq;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;

namespace ApplicationManager.Persistence.Identities
{
    public class GetIdentityBySessionQuery : IGetIdentityBySessionQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public GetIdentityBySessionQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public Identity Run(Guid sessionId)
        {
            return _applicationManagerDbContext.Identities.SingleOrDefault(i => i.Session.Id == sessionId);
        }
    }
}