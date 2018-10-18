using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.Authentication.Models;
using AlsTradingPost.Application.Claims;
using AlsTradingPost.Domain.PersonaDomain.Interfaces;
using AlsTradingPost.Domain.PersonaDomain.Models;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AlsTradingPost.Resources;
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
        private readonly IPersonaDomainService _personaDomainService;

        public AuthenticationApplicationService(
            IUserDomainService userDomainService,
            ICurrentIdentityProvider currentIdentityProvider,
            IJwtFactory jwtFactory,
            IValidator<LoginAdto> loginValidator,
            IValidator<RefreshTokenAdto> refreshTokenValidator,
            IMapper mapper,
            ISessionDomainService sessionDomainService,
            IPersonaDomainService personaDomainService)
        {
            _userDomainService = userDomainService;
            _currentIdentityProvider = currentIdentityProvider;
            _jwtFactory = jwtFactory;
            _loginValidator = loginValidator;
            _refreshTokenValidator = refreshTokenValidator;
            _mapper = mapper;
            _sessionDomainService = sessionDomainService;
            _personaDomainService = personaDomainService;
        }

        public async Task<JwtAdto> LoginAsync(LoginAdto loginAdto)
        {
            _loginValidator.ValidateAndThrow(loginAdto);
            
            LoginDdto loginDdto = Mapper.Map<LoginAdto, LoginDdto>(loginAdto);
            loginDdto.IdentityId = _currentIdentityProvider.Id;

            AuthenticatedUserProjection userProjection = _userDomainService.Login(loginDdto);

            JwtAdto jwt = await _jwtFactory.GenerateJwt<JwtAdto>(
                BuildClaims(userProjection.Id)
            );
		    
            SessionProjection createSessionProjection = _sessionDomainService.Create(userProjection.Id);
            
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
                    BuildClaims(refreshSessionProjection.Id)
                );

                jwt.RefreshToken = refreshSessionProjection.RefreshToken;
                jwt.SessionId = refreshSessionProjection.Id;

                return jwt;
            }
            catch (SessionDoesNotExistDomainException e)
            {
                throw new BusinessApplicationException(ExceptionType.NotFound, e);
            }
            catch (SessionRevokedDomainException e)
            {
                throw new BusinessApplicationException(ExceptionType.Unauthorized, BusinessErrorMessages.SessionRevoked, e);
            }
            catch (RefreshTokenInvalidDomainException e)
            {
                throw new BusinessApplicationException(ExceptionType.Unauthorized, BusinessErrorMessages.RefreshTokenInvalid, e);
            }
        }

        private ClaimsIdentity BuildClaims(Guid userId)
        {
            IEnumerable<PersonaProjection> personaProjections = _personaDomainService.GetUserPersonas(userId).ToArray();

            PersonaFlags personas = PersonTypeMapper.GetPersonaFlags(personaProjections.Select(u => u.PersonaType).ToArray());

            ClaimsBuilder claimsBuilder = ClaimsBuilder.CreateBuilder()
                .WithPersonas(personas);

            PersonaProjection trader = personaProjections.SingleOrDefault(p => p.PersonaType == PersonaType.Trader);
            PersonaProjection admin = personaProjections.SingleOrDefault(p => p.PersonaType == PersonaType.Admin);

            if (trader != null)
            {
                claimsBuilder.WithTrader(trader.Id);
            }

            if (admin != null)
            {
                claimsBuilder.WithAdmin(admin.Id);
            }

            return claimsBuilder
                .WithSubject(userId)
                .WithRole(JwtClaims.AppAccess)
                .Build();
        }
    }
}

