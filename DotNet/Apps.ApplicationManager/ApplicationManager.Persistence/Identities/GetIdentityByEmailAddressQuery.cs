using System.Linq;
using ApplicationManager.Domain;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.CheckPassword;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.DataProtection;

namespace ApplicationManager.Persistence.Identities
{
    public class GetIdentityByEmailAddressQuery : IGetIdentityByEmailAddressQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public GetIdentityByEmailAddressQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public Identity Run(string emailAddress)
        {
            return _applicationManagerDbContext.Identities
                .SingleOrDefault(i => i.EmailAddressHash == DataProtection.Hash(emailAddress, StaticSalts.EmailAddress).Hash);
        }
    }
}