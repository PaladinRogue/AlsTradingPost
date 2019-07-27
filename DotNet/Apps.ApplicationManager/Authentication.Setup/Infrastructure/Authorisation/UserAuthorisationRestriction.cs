using System.Threading.Tasks;
using Authentication.ApplicationServices;
using Authentication.Domain.Users;
using Common.Authorisation.Contexts;
using Common.Authorisation.Restrictions;
using Common.Domain.Persistence;

namespace Authentication.Setup.Infrastructure.Authorisation
{
    public class UserAuthorisationRestriction : IAuthorisationRestriction
    {
        private readonly ICurrentUserProvider _currentUserProvider;

        private readonly IQueryRepository<User> _userQueryRepository;

        public UserAuthorisationRestriction(
            ICurrentUserProvider currentUserProvider,
            IQueryRepository<User> userQueryRepository)
        {
            _currentUserProvider = currentUserProvider;
            _userQueryRepository = userQueryRepository;
        }

        public string Restriction => AuthorisationRestriction.User;

        public async Task<IRestrictionResult> CheckRestrictionAsync(IAuthorisationContext authorisationContext)
        {
            if (_currentUserProvider.IsAuthenticated && _currentUserProvider.Id.HasValue)
            {
                User user = await _userQueryRepository.GetByIdAsync(_currentUserProvider.Id.Value);

                return user == null || user.Identity.Session.IsRevoked ? RestrictionResult.Fail : RestrictionResult.Succeed;
            }

            return RestrictionResult.Fail;
        }
    }
}