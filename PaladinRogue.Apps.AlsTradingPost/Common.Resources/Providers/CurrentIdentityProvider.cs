using System;
using Common.Resources.Authentication;
using Common.Resources.Exceptions;
using Common.Resources.Extensions;
using Common.Resources.Providers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Common.Resources.Providers
{
    public class CurrentIdentityProvider : ICurrentIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtAuthenticationIssuerOptions _jwtAuthenticationIssuerOptionsAccessor;

        public CurrentIdentityProvider(IHttpContextAccessor httpContextAccessor, IOptions<JwtAuthenticationIssuerOptions> jwtAuthenticationIssuerOptionsAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtAuthenticationIssuerOptionsAccessor = jwtAuthenticationIssuerOptionsAccessor.Value;
        }

        public Guid Id
        {
            get
            {
                if (_httpContextAccessor.CurrentIssuer() == _jwtAuthenticationIssuerOptionsAccessor.Issuer)
                {
                    Guid? currentUserId = _httpContextAccessor.CurrentSubject();
                    if (currentUserId.HasValue)
                    {
                        return currentUserId.Value;
                    }
                }

                throw new AppException(ExceptionType.Unauthorized, "Current identity token is not valid");
            }
        }
    }
}
