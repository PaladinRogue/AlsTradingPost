using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Common.Api.Factories.Interfaces;

namespace AlsTradingPost.Api.Factories
{
    public class ClaimsFactory : IClaimsFactory
    {
        public ClaimsIdentity GenerateClaimsIdentity(Guid id)
        {
            return new ClaimsIdentity(new GenericIdentity(id.ToString(), "Token"), new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString())
            });
        }
    }
}
