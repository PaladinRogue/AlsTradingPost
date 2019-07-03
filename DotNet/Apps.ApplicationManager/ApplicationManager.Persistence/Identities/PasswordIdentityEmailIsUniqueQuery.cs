using System.Linq;
using ApplicationManager.Domain;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.DataProtection;

namespace ApplicationManager.Persistence.Identities
{
    public class PasswordIdentityEmailIsUniqueQuery : IPasswordIdentityEmailIsUniqueQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public PasswordIdentityEmailIsUniqueQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public bool Run(string emailAddress)
        {
            return _applicationManagerDbContext.Identities
                .Any(i => i.AuthenticationIdentities.OfType<PasswordIdentity>().Any(a => a.EmailAddressHash == DataProtection.Hash(emailAddress, StaticSalts.EmailAddress).Hash));
        }
    }
}