﻿using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PaladinRogue.Library.Core.Application.Authentication;
using PaladinRogue.Library.Core.Application.Exceptions;
using PaladinRogue.Library.Core.Setup.Infrastructure.HttpContexts;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Authorisation
{
    public class CurrentIdentityProvider : ICurrentIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly JwtIssuerOptions _jwtIssuerOptions;

        public CurrentIdentityProvider(IHttpContextAccessor httpContextAccessor, IOptions<JwtIssuerOptions> jwtIssuerOptionsAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtIssuerOptions = jwtIssuerOptionsAccessor.Value;
        }

        public bool IsAuthenticated => _httpContextAccessor.IsAuthenticated();

        public Guid Id
        {
            get
            {
                if (_httpContextAccessor.CurrentIssuer() != _jwtIssuerOptions.Issuer)
                {
                    throw new BusinessApplicationException(ExceptionType.Unauthorized, "Current identity token is not valid");
                }

                Guid? currentIdentityId = _httpContextAccessor.CurrentSubject();
                if (currentIdentityId.HasValue)
                {
                    return currentIdentityId.Value;
                }

                throw new BusinessApplicationException(ExceptionType.Unauthorized, "Current identity token is not valid");
            }
        }
    }
}
