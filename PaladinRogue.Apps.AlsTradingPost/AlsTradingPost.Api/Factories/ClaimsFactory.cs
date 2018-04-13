using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using AlsTradingPost.Application.User.Interfaces;
using AlsTradingPost.Application.User.Models;
using AlsTradingPost.Resources.Constants;
using Common.Api.Factories.Interfaces;

namespace AlsTradingPost.Api.Factories
{
    public class ClaimsFactory : IClaimsFactory
    {
        private readonly IUserApplicationService _userApplicationService;

        public ClaimsFactory(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        public ClaimsIdentity GenerateClaimsIdentity(Guid id)
        {
            UserAdto user = _userApplicationService.Get(id);
            
            return new ClaimsIdentity(new GenericIdentity(id.ToString(), "Token"), new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtClaims.Persona, user.Personas.ToString())
            });
        }
    }
}
