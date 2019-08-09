using System;
using Authentication.Application.Claims;
using Common.Application.Authentication;
using Common.Application.Exceptions;
using Common.Resources.Extensions;
using Common.Setup.Infrastructure.HttpContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Authentication.Setup.Infrastructure.Authorisation
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

        public bool IsAuthenticated => _httpContextAccessor.IsAuthenticated();

        public Guid? Id
        {
            get
            {
                if (_httpContextAccessor.CurrentIssuer() != _jwtIssuerOptions.Issuer)
                {
                    throw new BusinessApplicationException(ExceptionType.Unauthorized, "Current identity token is not valid");
                }

                return _httpContextAccessor.GetClaim(JwtClaimIdentifiers.User);
            }
        }
    }
}
