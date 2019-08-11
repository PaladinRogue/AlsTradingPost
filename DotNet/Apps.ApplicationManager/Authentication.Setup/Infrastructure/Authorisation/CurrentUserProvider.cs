using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PaladinRogue.Authentication.Application.Claims;
using PaladinRogue.Libray.Core.Application.Authentication;
using PaladinRogue.Libray.Core.Application.Exceptions;
using PaladinRogue.Libray.Core.Setup.Infrastructure.HttpContexts;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Authorisation
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
