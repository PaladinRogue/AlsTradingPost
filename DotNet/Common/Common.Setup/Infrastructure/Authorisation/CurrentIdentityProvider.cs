using System;
using Common.Application.Exceptions;
using Common.Resources.Extensions;
using Common.Setup.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Common.Setup.Infrastructure.Authorisation
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

                throw new BusinessApplicationException(ExceptionType.Unauthorized, "Current identity token is not valid");
            }
        }
    }
}
