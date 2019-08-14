using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaladinRogue.Authentication.Domain;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.Identities.Queries;
using PaladinRogue.Library.Core.Domain.DataProtectors;

namespace PaladinRogue.Authentication.Persistence.Identities
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