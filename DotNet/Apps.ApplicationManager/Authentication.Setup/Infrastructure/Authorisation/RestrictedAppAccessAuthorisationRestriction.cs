using System.Threading.Tasks;
using Authentication.ApplicationServices;
using Authentication.Domain.Identities;
using Common.Authorisation.Contexts;
using Common.Authorisation.Restrictions;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Authorisation;

namespace Authentication.Setup.Infrastructure.Authorisation
{
    public class RestrictedAppAccessAuthorisationRestriction : IAuthorisationRestriction
    {
        private readonly ICurrentIdentityProvider _currentIdentityProvider;

        private readonly IQueryRepository<Identity> _identityQueryRepository;

        public RestrictedAppAccessAuthorisationRestriction(
            ICurrentIdentityProvider currentIdentityProvider,
            IQueryRepository<Identity> identityQueryRepository)
        {
            _currentIdentityProvider = currentIdentityProvider;
            _identityQueryRepository = identityQueryRepository;
        }

        public string Restriction => AuthorisationRestriction.RestrictedAppAccess;

        public async Task<IRestrictionResult> CheckRestrictionAsync(IAuthorisationContext authorisationContext)
        {
            if (_currentIdentityProvider.IsAuthenticated)
            {
                Identity identity = await _identityQueryRepository.GetByIdAsync(_currentIdentityProvider.Id);

                return identity != null && identity.IsConfirmed ? RestrictionResult.Fail : RestrictionResult.Succeed;
            }

            return RestrictionResult.Fail;
        }
    }
}