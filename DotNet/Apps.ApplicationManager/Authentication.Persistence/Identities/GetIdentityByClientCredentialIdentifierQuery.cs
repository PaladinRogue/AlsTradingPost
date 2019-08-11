using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaladinRogue.Authentication.Domain;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.Identities.Queries;
using PaladinRogue.Libray.Core.Domain.DataProtectors;

namespace PaladinRogue.Authentication.Persistence.Identities
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