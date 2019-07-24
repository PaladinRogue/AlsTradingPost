using System.Linq;
using System.Threading.Tasks;
using Authentication.Domain.Identities;
using Authentication.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

namespace Authtentication.Persistence.Identities
{
    public class PasswordIdentityIdentifierExistsQuery : IPasswordIdentityIdentifierExistsQuery
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public PasswordIdentityIdentifierExistsQuery(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public async Task<bool> RunAsync(string identifier)
        {
            return await _authenticationDbContext.Identities.AnyAsync(i => i.AuthenticationIdentities.OfType<PasswordIdentity>().Any(p => p.Identifier == identifier));
        }
    }
}