using System.Threading.Tasks;
using Authentication.ApplicationServices;
using Authentication.Domain.Identities;
using Common.ApplicationServices.Transactions;
using Common.Authorisation.Contexts;
using Common.Authorisation.Restrictions;
using Common.Domain.Persistence;
using Common.Setup.Infrastructure.Authorisation;

namespace Authentication.Setup.Infrastructure.Authorisation
{
    public class RestrictedAppAccessAuthorisationRestriction : IAuthorisationRestriction
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICurrentIdentityProvider _currentIdentityProvider;

        private readonly IQueryRepository<Identity> _identityQueryRepository;

        public RestrictedAppAccessAuthorisationRestriction(
            ITransactionManager transactionManager,
            ICurrentIdentityProvider currentIdentityProvider,
            IQueryRepository<Identity> identityQueryRepository)
        {
            _transactionManager = transactionManager;
            _currentIdentityProvider = currentIdentityProvider;
            _identityQueryRepository = identityQueryRepository;
        }

        public string Restriction => AuthorisationRestriction.RestrictedAppAccess;

        public async Task<IRestrictionResult> CheckRestrictionAsync(IAuthorisationContext authorisationContext)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                if (_currentIdentityProvider.IsAuthenticated)
                {
                    Identity identity = await _identityQueryRepository.GetByIdAsync(_currentIdentityProvider.Id);

                    transaction.Commit();

                    return identity == null || identity.IsConfirmed || identity.Session.IsRevoked ? RestrictionResult.Fail : RestrictionResult.Succeed;
                }

                return RestrictionResult.Fail;
            }
        }
    }
}