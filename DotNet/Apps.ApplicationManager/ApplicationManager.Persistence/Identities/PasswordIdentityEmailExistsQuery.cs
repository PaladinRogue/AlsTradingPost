using System.Linq;
using System.Threading.Tasks;
using ApplicationManager.Domain;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManager.Persistence.Identities
{
    public class PasswordIdentityEmailExistsQuery : IPasswordIdentityEmailExistsQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public PasswordIdentityEmailExistsQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public Task<bool> RunAsync(string emailAddress)
        {
            return _applicationManagerDbContext.Identities
                .AnyAsync(i => i.AuthenticationIdentities.OfType<PasswordIdentity>().Any(a => a.EmailAddressHash == DataProtection.Hash(emailAddress, StaticSalts.EmailAddress).Hash));
        }
    }
}