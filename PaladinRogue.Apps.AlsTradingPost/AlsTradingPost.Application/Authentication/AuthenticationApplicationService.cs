using System.Threading.Tasks;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.Authentication.Models;
using AlsTradingPost.Application.Claims;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AutoMapper;
using Common.Api.Authentication.Constants;
using Common.Application.Authentication;
using Common.Application.Exceptions;
using Common.Authentication.Domain.SessionDomain.Exceptions;
using Common.Authentication.Domain.SessionDomain.Interfaces;
using Common.Authentication.Domain.SessionDomain.Models;
using Common.Setup.Infrastructure.Authorization;
using FluentValidation;

namespace AlsTradingPost.Application.Authentication
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {
        private readonly IUserDomainService _userDomainService;
        private readonly ICurrentIdentityProvider _currentIdentityProvider;
        private readonly IJwtFactory _jwtFactory;
        private readonly IValidator<LoginAdto> _loginValidator;
        private readonly IValidator<RefreshTokenAdto> _refreshTokenValidator;
        private readonly IMapper _mapper;
        private readonly ISessionDomainService _sessionDomainService;

        public AuthenticationApplicationService(
            IUserDomainService userDomainService,
            ICurrentIdentityProvider currentIdentityProvider,
            IJwtFactory jwtFactory,
            IValidator<LoginAdto> loginValidator,
            IValidator<RefreshTokenAdto> refreshTokenValidator,
            IMapper mapper,
            ISessionDomainService sessionDomainService)
        {
            _userDomainService = userDomainService;
            _currentIdentityProvider = currentIdentityProvider;
            _jwtFactory = jwtFactory;
            _loginValidator = loginValidator;
            _refreshTokenValidator = refreshTokenValidator;
            _mapper = mapper;
            _sessionDomainService = sessionDomainService;
        }

        public async Task<JwtAdto> LoginAsync(LoginAdto loginAdto)
        {
            _loginValidator.ValidateAndThrow(loginAdto);
            
            LoginDdto loginDdto = Mapper.Map<LoginAdto, LoginDdto>(loginAdto);
            loginDdto.IdentityId = _currentIdentityProvider.Id;

            AuthenticatedUserProjection userProjection = _userDomainService.Login(loginDdto);

            JwtAdto jwt = await _jwtFactory.GenerateJwt<JwtAdto>(
                ClaimsBuilder.CreateBuilder()
                    .WithPersonas(userProjection.Personas)
                    .WithSubject(userProjection.Id)
                    .WithRole(JwtClaims.AppAccess)
                    .Build()
            );
		    
            CreateSessionProjection createSessionProjection = _sessionDomainService.Create(userProjection.Id);
            
            jwt.RefreshToken = createSessionProjection.RefreshToken;
            jwt.SessionId = createSessionProjection.Id;

            return jwt;
        }

        public async Task<JwtAdto> RefreshTokenAsync(RefreshTokenAdto refreshTokenAdto)
        {
            _refreshTokenValidator.ValidateAndThrow(refreshTokenAdto);
		    
            try
            {
                RefreshSessionProjection refreshSessionProjection =
                    _sessionDomainService.Refresh(_mapper.Map<RefreshTokenAdto, RefreshSessionDdto>(refreshTokenAdto));

                JwtAdto jwt = await _jwtFactory.GenerateJwt<JwtAdto>(
                    ClaimsBuilder.CreateBuilder()
                        .WithSubject(refreshSessionProjection.Id)
                        .WithRole(JwtClaims.AppAccess)
                        .Build()
                );

                jwt.RefreshToken = refreshSessionProjection.RefreshToken;
                jwt.SessionId = refreshSessionProjection.Id;

                return jwt;
            }
            catch (SessionRevokedDomainException e)
            {
                throw new ApplicationException(ExceptionType.Unauthorized, e);
            }
            catch (RefreshTokenInvalidDomainException e)
            {
                throw new ApplicationException(ExceptionType.Unauthorized, e);
            }
        }
    }
}

