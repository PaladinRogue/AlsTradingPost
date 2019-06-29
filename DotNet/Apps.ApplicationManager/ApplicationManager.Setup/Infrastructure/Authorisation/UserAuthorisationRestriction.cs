using ApplicationManager.ApplicationServices;
using ApplicationManager.Domain.Users;
using Common.Authorisation.Contexts;
using Common.Authorisation.Restrictions;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Authorisation;

namespace ApplicationManager.Setup.Infrastructure.Authorisation
{
    public class UserAuthorisationRestriction : IAuthorisationRestriction
    {
        private readonly ICurrentIdentityProvider _currentIdentityProvider;

        private readonly IQueryRepository<User> _userQueryRepository;

        public UserAuthorisationRestriction(
            ICurrentIdentityProvider currentIdentityProvider,
            IQueryRepository<User> userQueryRepository)
        {
            _currentIdentityProvider = currentIdentityProvider;
            _userQueryRepository = userQueryRepository;
        }

        public string Restriction => AuthorisationRestriction.User;

        public IRestrictionResult CheckRestriction(IAuthorisationContext authorisationContext)
        {
            User user = _userQueryRepository.GetSingle(u => u.Identity.Id == _currentIdentityProvider.Id);

            return user == null ? RestrictionResult.Fail : RestrictionResult.Succeed;
        }
    }
}