using System.Linq;
using System.Threading.Tasks;
using ApplicationManager.Domain;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManager.Persistence.Identities
{
    public class GetIdentityByClientCredentialIdentifierQuery : IGetIdentityByClientCredentialIdentifierQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public GetIdentityByClientCredentialIdentifierQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public Task<Identity> RunAsync(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            string identifier)
        {
            return _applicationManagerDbContext.Identities
                .SingleOrDefaultAsync(i =>
                    i.AuthenticationIdentities.OfType<ClientCredentialIdentity>().Any(a =>
                        a.IdentifierHash == DataProtection.Hash(identifier, StaticSalts.Identifier).Hash
                        && a.AuthenticationGrantTypeClientCredential == authenticationGrantTypeClientCredential));
        }
    }
}