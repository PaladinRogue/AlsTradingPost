using System.Linq;
using ApplicationManager.Domain;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.DataProtection;

namespace ApplicationManager.Persistence.Identities
{
    public class GetIdentityByClientCredentialIdentifierQuery : IGetIdentityByClientCredentialIdentifierQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public GetIdentityByClientCredentialIdentifierQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public Identity Run(AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            string identifier)
        {
            return _applicationManagerDbContext.Identities
                .SingleOrDefault(i =>
                    i.AuthenticationIdentities.OfType<ClientCredentialIdentity>().Any(a =>
                        a.IdentifierHash == DataProtection.Hash(identifier, StaticSalts.Identifier).Hash
                        && a.AuthenticationGrantTypeClientCredential == authenticationGrantTypeClientCredential));
        }
    }
}