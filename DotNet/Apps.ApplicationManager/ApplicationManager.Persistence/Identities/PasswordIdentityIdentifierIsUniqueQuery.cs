using System.Linq;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.Persistence;

namespace ApplicationManager.Persistence.Identities
{
    public class PasswordIdentityIdentifierIsUniqueQuery : IPasswordIdentityIdentifierIsUniqueQuery
    {
        private readonly IQueryRepository<Identity> _queryRepository;

        public PasswordIdentityIdentifierIsUniqueQuery(IQueryRepository<Identity> queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public bool Run(string identifier)
        {
            return _queryRepository.CheckExists(i => i.AuthenticationIdentities.Any(ai => (ai is PasswordIdentity) && (ai as PasswordIdentity).Identifier == identifier));
        }
    }
}