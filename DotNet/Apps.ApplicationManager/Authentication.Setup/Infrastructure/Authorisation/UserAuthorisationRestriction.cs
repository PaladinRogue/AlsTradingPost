using System.Threading.Tasks;
using PaladinRogue.Authentication.Application;
using PaladinRogue.Authentication.Domain.Users;
using PaladinRogue.Libray.Authorisation.Common.Contexts;
using PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Restrictions;
using PaladinRogue.Libray.Core.Domain.Persistence;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Authorisation
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