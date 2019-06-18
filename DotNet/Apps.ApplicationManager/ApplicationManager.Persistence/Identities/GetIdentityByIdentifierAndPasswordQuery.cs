using System.Linq;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;

namespace ApplicationManager.Persistence.Identities
{
    public class GetIdentityByIdentifierAndPasswordQuery : IGetIdentityByIdentifierAndPasswordQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public GetIdentityByIdentifierAndPasswordQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public Identity Run(string identifier, string password)
        {
             Identity identity = _applicationManagerDbContext.Identities
                 .SingleOrDefault(i => i.AuthenticationIdentities.OfType<PasswordIdentity>().Any(p => p.Identifier == identifier));

             if (identity == null)
             {
                 return null;
             }

             PasswordIdentity passwordIdentity = identity.AuthenticationIdentities.OfType<PasswordIdentity>().Single();

             return passwordIdentity.CheckPassword(password) ? identity : null;
        }
    }
}