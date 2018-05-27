using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Resources.Authorization;
using Common.Application.Authentication;
using Common.Application.Exceptions;
using Common.Resources.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AlsTradingPost.Setup.Infrastructure.Authorisation
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtIssuerOptions _jwtIssuerOptions;
        private IReadOnlyDictionary<Type, Guid> _whoAmI;

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

                throw new BusinessApplicationException(ExceptionType.Unauthorized, "Current user token is not valid");
            }
        }

        public IReadOnlyDictionary<Type, Guid> WhoAmI
        {
            get
            {
                Dictionary<Type, Guid> whoAmI = new Dictionary<Type, Guid>();
                if (_httpContextAccessor.CurrentIssuer() == _jwtIssuerOptions.Issuer)
                {
                    Guid? currentUserId = _httpContextAccessor.CurrentSubject();
                    if (currentUserId.HasValue)
                    {
                        whoAmI.Add(typeof(Admin), currentUserId.Value);
                    }
                }

                return whoAmI;
            }
        }
    }
}
