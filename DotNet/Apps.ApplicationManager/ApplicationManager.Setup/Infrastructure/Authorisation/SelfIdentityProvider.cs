using System;
using System.Collections.Generic;
using System.Linq;
using Common.Authorisation;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public IReadOnlyDictionary<Type, Guid> WhoAmI =>
            new Dictionary<Type, Guid>
            {
                [typeof(Identity)] = _currentIdentityProvider.Id
            };
    }
}