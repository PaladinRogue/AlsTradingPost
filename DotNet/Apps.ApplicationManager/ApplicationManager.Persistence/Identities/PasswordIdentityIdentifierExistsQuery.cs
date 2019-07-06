using System.Linq;
using System.Threading.Tasks;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManager.Persistence.Identities
{
    public class PasswordIdentityIdentifierExistsQuery : IPasswordIdentityIdentifierExistsQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public PasswordIdentityIdentifierExistsQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public async Task<bool> RunAsync(string identifier)
        {
            return await _applicationManagerDbContext.Identities.AnyAsync(i => i.AuthenticationIdentities.OfType<PasswordIdentity>().Any(p => p.Identifier == identifier));
        }
    }
}