﻿using System;
using Common.ApplicationServices.Authentication;
using Common.ApplicationServices.Exceptions;
using Common.Resources.Extensions;
using Common.Setup.Infrastructure.HttpContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Common.Setup.Infrastructure.Authorisation
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
