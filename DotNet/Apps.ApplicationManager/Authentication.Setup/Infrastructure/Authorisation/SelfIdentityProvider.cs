using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Domain.Identities;
using Authentication.Domain.Users;
using Common.ApplicationServices.Exceptions;
using Common.Authorisation;
using Common.Domain.Persistence;
using Common.Resources.Builders.Dictionaries;
using Common.Setup.Infrastructure.Authorisation;

namespace Authentication.Setup.Infrastructure.Authorisation
{
    public class SelfIdentityProvider : ISelfProvider
    {
        private readonly ICurrentIdentityProvider _currentIdentityProvider;

        private readonly ICurrentUserProvider _currentUserProvider;

        private readonly IQueryRepository<Identity> _identityQueryRepository;

        private readonly IQueryRepository<User> _userQueryRepository;

        public SelfIdentityProvider(
            ICurrentIdentityProvider currentIdentityProvider,
            IQueryRepository<Identity> identityQueryRepository,
            IQueryRepository<User> userQueryRepository,
            ICurrentUserProvider currentUserProvider)
        {
            _currentIdentityProvider = currentIdentityProvider;
            _identityQueryRepository = identityQueryRepository;
            _userQueryRepository = userQueryRepository;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<IDictionary<Type, Guid>> WhoAmIAsync()
        {
            if (!_currentIdentityProvider.IsAuthenticated)
            {
                return new Dictionary<Type, Guid>();
            }

            Identity identity = await _identityQueryRepository.GetByIdAsync(_currentIdentityProvider.Id);

            if (identity == null)
            {
                throw new BusinessApplicationException(ExceptionType.Unauthorized, "Current identity token is not valid");
            }

            DictionaryBuilder<Type, Guid> whoAmI = DictionaryBuilder<Type, Guid>.Create();

            whoAmI.Add(typeof(Identity), identity.Id);

            if (_currentUserProvider.Id.HasValue)
            {
                User user = await _userQueryRepository.GetByIdAsync(_currentUserProvider.Id.Value);

                if (user != null)
                {
                    whoAmI.Add(typeof(User), user.Id);
                }
            }

            return whoAmI.Build();
        }
    }
}