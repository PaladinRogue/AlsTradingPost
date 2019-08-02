using System.Linq;
using System.Threading.Tasks;
using Authentication.Domain;
using Authentication.Domain.AuthenticationServices;
using Authentication.Domain.Identities;
using Authentication.Domain.Identities.Queries;
using Common.Domain.DataProtectors;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.Identities
{
    public class GetIdentityByClientCredentialIdentifierQuery : IGetIdentityByClientCredentialIdentifierQuery
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public GetIdentityByClientCredentialIdentifierQuery(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public async Task<Identity> RunAsync(
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            string identifier)
        {
            HashSet identifierHash = await DataProtection.StaticHashAsync(identifier, DataKeys.IdentifierSalt);

            return await _authenticationDbContext.Identities
                .SingleOrDefaultAsync(i =>
                    i.AuthenticationIdentities.OfType<ClientCredentialIdentity>().Any(a =>
                        a.IdentifierHash == identifierHash.Hash
                        && a.AuthenticationGrantTypeClientCredential == authenticationGrantTypeClientCredential));
        }
    }
}