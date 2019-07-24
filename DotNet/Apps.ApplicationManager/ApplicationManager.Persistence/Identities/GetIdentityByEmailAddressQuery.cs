using System.Linq;
using System.Threading.Tasks;
using ApplicationManager.Domain;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.DataProtectors;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManager.Persistence.Identities
{
    public class GetIdentityByEmailAddressQuery : IGetIdentityByEmailAddressQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public GetIdentityByEmailAddressQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public async Task<Identity> RunAsync(string emailAddress)
        {
            HashSet emailAddressHash = await DataProtection.StaticHashAsync(emailAddress, DataKeys.EmailAddressSalt);

            return await _applicationManagerDbContext.Identities
                .SingleOrDefaultAsync(i => i.AuthenticationIdentities.OfType<PasswordIdentity>().Any(
                    a => a.EmailAddressHash == emailAddressHash.Hash
                ));
        }
    }
}