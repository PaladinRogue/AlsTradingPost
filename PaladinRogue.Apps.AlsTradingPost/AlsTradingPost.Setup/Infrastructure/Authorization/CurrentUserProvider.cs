﻿using System;
using Common.Application.Authentication;
using Common.Application.Exceptions;
using Common.Resources.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ApplicationException = Common.Application.Exceptions.ApplicationException;

namespace AlsTradingPost.Setup.Infrastructure.Authorization
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtIssuerOptions _jwtIssuerOptions;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor, IOptions<JwtIssuerOptions> jwtIssuerOptionsAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtIssuerOptions = jwtIssuerOptionsAccessor.Value;
        }

        public Guid Id
        {
            get
            {
                if (_httpContextAccessor.CurrentIssuer() == _jwtIssuerOptions.Issuer)
                {
                    Guid? currentUserId = _httpContextAccessor.CurrentSubject();
                    if (currentUserId.HasValue)
                    {
                        return currentUserId.Value;
                    }
                }

                throw new ApplicationException(ExceptionType.Unauthorized, "Current user token is not valid");
            }
        }
    }
}