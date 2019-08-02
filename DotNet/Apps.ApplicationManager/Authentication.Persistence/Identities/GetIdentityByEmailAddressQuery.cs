using System.Linq;
using System.Threading.Tasks;
using Authentication.Domain;
using Authentication.Domain.Identities;
using Authentication.Domain.Identities.Queries;
using Common.Domain.DataProtectors;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.Identities
{
    public class GetIdentityByEmailAddressQuery : IGetIdentityByEmailAddressQuery
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public GetIdentityByEmailAddressQuery(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public async Task<Identity> RunAsync(string emailAddress)
        {
            HashSet emailAddressHash = await DataProtection.StaticHashAsync(emailAddress, DataKeys.EmailAddressSalt);

            return await _authenticationDbContext.Identities
                .SingleOrDefaultAsync(i => i.AuthenticationIdentities.OfType<PasswordIdentity>().Any(
                    a => a.EmailAddressHash == emailAddressHash.Hash
                ));
        }
    }
}