using System.Linq;
using System.Threading.Tasks;
using Authentication.Domain;
using Authentication.Domain.Identities;
using Authentication.Domain.Identities.Queries;
using Common.Domain.DataProtectors;
using Microsoft.EntityFrameworkCore;

namespace Authtentication.Persistence.Identities
{
    public class PasswordIdentityEmailExistsQuery : IPasswordIdentityEmailExistsQuery
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public PasswordIdentityEmailExistsQuery(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public async Task<bool> RunAsync(string emailAddress)
        {
            HashSet emailAddressHash = await DataProtection.StaticHashAsync(emailAddress, DataKeys.EmailAddressSalt);

            return await _authenticationDbContext.Identities
                .AnyAsync(i => i.AuthenticationIdentities.OfType<PasswordIdentity>()
                    .Any(a => a.EmailAddressHash == emailAddressHash.Hash)
                );
        }
    }
}