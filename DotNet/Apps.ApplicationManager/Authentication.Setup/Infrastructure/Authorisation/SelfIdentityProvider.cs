using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.Users;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Self;
using PaladinRogue.Library.Core.Application.Exceptions;
using PaladinRogue.Library.Core.Application.Transactions;
using PaladinRogue.Library.Core.Common.Builders.Dictionaries;
using PaladinRogue.Library.Core.Domain.Persistence;
using PaladinRogue.Library.Core.Setup.Infrastructure.Authorisation;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Authorisation
{
    public class SelfIdentityProvider : ISelfProvider
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ICurrentIdentityProvider _currentIdentityProvider;

        private readonly ICurrentUserProvider _currentUserProvider;

        private readonly IQueryRepository<Identity> _identityQueryRepository;

        private readonly IQueryRepository<User> _userQueryRepository;

        public SelfIdentityProvider(
            ITransactionManager transactionManager,
            ICurrentIdentityProvider currentIdentityProvider,
            IQueryRepository<Identity> identityQueryRepository,
            IQueryRepository<User> userQueryRepository,
            ICurrentUserProvider currentUserProvider)
        {
            _transactionManager = transactionManager;
            _currentIdentityProvider = currentIdentityProvider;
            _identityQueryRepository = identityQueryRepository;
            _userQueryRepository = userQueryRepository;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<IDictionary<Type, Guid>> WhoAmIAsync()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                if (!_currentIdentityProvider.IsAuthenticated)
                {
                    return new Dictionary<Type, Guid>();
                }

                Identity identity = await _identityQueryRepository.GetByIdAsync(_currentIdentityProvider.Id);

                if (identity == null || identity.Session.IsRevoked)
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

                transaction.Commit();

                return whoAmI.Build();
            }
        }
    }
}