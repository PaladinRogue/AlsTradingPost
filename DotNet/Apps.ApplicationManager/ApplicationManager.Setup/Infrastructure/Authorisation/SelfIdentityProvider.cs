using System;
using System.Collections.Generic;
using Common.Authorisation;
using ApplicationManager.Domain.Identities;
using Common.Setup.Infrastructure.Authorisation;

namespace ApplicationManager.Setup.Infrastructure.Authorisation
{
    public class SelfIdentityProvider : ISelfProvider
    {
        private readonly ICurrentIdentityProvider _currentIdentityProvider;

        public SelfIdentityProvider(ICurrentIdentityProvider currentIdentityProvider)
        {
            _currentIdentityProvider = currentIdentityProvider;
        }

        //TODO Should this validate against the DB or rely on the auth token?
        public IReadOnlyDictionary<Type, Guid> WhoAmI =>
            new Dictionary<Type, Guid>
            {
                [typeof(Identity)] = _currentIdentityProvider.Id
            };
    }
}