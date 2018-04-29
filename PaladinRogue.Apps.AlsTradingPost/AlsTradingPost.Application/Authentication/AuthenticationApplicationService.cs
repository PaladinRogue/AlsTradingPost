using System.Threading.Tasks;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.Authentication.Models;
using AlsTradingPost.Application.Claims;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AlsTradingPost.Resources;
using AutoMapper;
using Common.Api.Authentication.Constants;
using Common.Application.Authentication;
using Common.Setup.Infrastructure.Authorization;

namespace AlsTradingPost.Application.Authentication
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {
        private readonly IUserDomainService _userDomainService;
        private readonly ICurrentIdentityProvider _currentIdentityProvider;
        private readonly IJwtFactory _jwtFactory;

        public AuthenticationApplicationService(
            IUserDomainService userDomainService,
            ICurrentIdentityProvider currentIdentityProvider,
            IJwtFactory jwtFactory)
        {
            _userDomainService = userDomainService;
            _currentIdentityProvider = currentIdentityProvider;
            _jwtFactory = jwtFactory;
        }

        public async Task<JwtAdto> LoginAsync(LoginAdto loginAdto)
        {
            LoginDdto loginDdto = Mapper.Map<LoginAdto, LoginDdto>(loginAdto);
            loginDdto.IdentityId = _currentIdentityProvider.Id;

            UserProjection userProjection = _userDomainService.Login(loginDdto);
            
            return await _jwtFactory.GenerateJwt<JwtAdto>(
                ClaimsBuilder.CreateBuilder()
                    .WithPersonas(Persona.Player)
                    .WithSubject(userProjection.Id)
                    .WithRole(JwtClaims.AppAccess)
                    .Build()
            );
        }
    }
}

