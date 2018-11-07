﻿using System.Threading.Tasks;
using Authentication.Application.Authentication.Interfaces;
using Authentication.Application.Authentication.Models;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using AutoMapper;
using Common.Api.Authentication.Constants;
using Common.Application.Authentication;
using Common.Application.Claims;
using Common.Application.Exceptions;
using Common.Authentication.Domain.SessionDomain.Exceptions;
using Common.Authentication.Domain.SessionDomain.Interfaces;
using Common.Authentication.Domain.SessionDomain.Models;
using Common.Setup.Infrastructure.Encryption.Interfaces;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Authentication.Application.Authentication
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {
        private readonly IIdentityDomainService _identityDomainService;
        private readonly IEncryptionFactory _encryptionFactory;
        private readonly JwtIssuerOptions _jwtIssuerOptions;
        private readonly IJwtFactory _jwtFactory;
        private readonly ISessionDomainService _sessionDomainService;
        private readonly IMapper _mapper;
        private readonly IValidator<RefreshTokenAdto> _refreshTokenValidator;
        private readonly IValidator<LoginAdto> _loginValidator;

        public AuthenticationApplicationService(
            IIdentityDomainService identityDomainService,
	        IJwtFactory jwtFactory,
	        IOptions<JwtIssuerOptions> jwtIssuerOptionsAccessor,
	        IEncryptionFactory encryptionFactory,
            ISessionDomainService sessionDomainService,
            IMapper mapper,
            IValidator<RefreshTokenAdto> refreshTokenValidator,
            IValidator<LoginAdto> loginValidator)
        {
            _identityDomainService = identityDomainService;
	        _jwtFactory = jwtFactory;
	        _jwtIssuerOptions = jwtIssuerOptionsAccessor.Value;
	        _encryptionFactory = encryptionFactory;
	        _sessionDomainService = sessionDomainService;
	        _mapper = mapper;
	        _refreshTokenValidator = refreshTokenValidator;
	        _loginValidator = loginValidator;
        }

	    public async Task<ExtendedJwtAdto> LoginAsync(LoginAdto loginAdto)
	    {
		    _loginValidator.ValidateAndThrow(loginAdto);

	        AuthenticatedIdentityProjection loginIdentityProjection = _identityDomainService.Login(Mapper.Map<LoginAdto, LoginDdto>(loginAdto));

	        ExtendedJwtAdto jwt = await _jwtFactory.GenerateJwt<ExtendedJwtAdto>(
	            ClaimsBuilder.CreateBuilder()
		            .WithSubject(loginIdentityProjection.Id)
		            .WithRole(JwtClaims.AppAccess)
		            .Build()
	        );
		    
		    SessionProjection createSessionProjection = _sessionDomainService.Create(loginIdentityProjection.Id);

	        jwt.AccessToken = _encryptionFactory.Enrypt(loginAdto.AccessToken, _jwtIssuerOptions.SigningKey);
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
			    throw new BusinessApplicationException(ExceptionType.Unauthorized, e);
		    }
		    catch (RefreshTokenInvalidDomainException e)
		    {
			    throw new BusinessApplicationException(ExceptionType.Unauthorized, e);
		    }
	    }
    }
}
