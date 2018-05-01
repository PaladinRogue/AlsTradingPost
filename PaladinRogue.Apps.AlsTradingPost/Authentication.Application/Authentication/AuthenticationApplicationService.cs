using System.Threading.Tasks;
using Authentication.Application.Authentication.Interfaces;
using Authentication.Application.Authentication.Models;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using AutoMapper;
using Common.Api.Authentication.Constants;
using Common.Application.Authentication;
using Common.Application.Claims;
using Common.Setup.Infrastructure.Encryption.Interfaces;
using Microsoft.Extensions.Options;

namespace Authentication.Application.Authentication
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {
        private readonly IIdentityDomainService _identityDomainService;
        private readonly IEncryptionFactory _encryptionFactory;
        private readonly JwtIssuerOptions _jwtIssuerOptions;
        private readonly IJwtFactory _jwtFactory;

        public AuthenticationApplicationService(
            IIdentityDomainService identityDomainService,
	        IJwtFactory jwtFactory,
	        IOptions<JwtIssuerOptions> jwtIssuerOptionsAccessor,
	        IEncryptionFactory encryptionFactory)
        {
            _identityDomainService = identityDomainService;
	        _jwtFactory = jwtFactory;
	        _jwtIssuerOptions = jwtIssuerOptionsAccessor.Value;
	        _encryptionFactory = encryptionFactory;
        }

	    public async Task<ExtendedJwtAdto> LoginAsync(LoginAdto loginAdto)
	    {
	        LoginIdentityProjection identityProjection = _identityDomainService.Login(Mapper.Map<LoginAdto, LoginDdto>(loginAdto));

	        ExtendedJwtAdto jwt = await _jwtFactory.GenerateJwt<ExtendedJwtAdto>(
	            ClaimsBuilder.CreateBuilder()
		            .WithSubject(identityProjection.Id)
		            .WithRole(JwtClaims.AppAccess)
		            .Build()
	        );

	        jwt.AccessToken = _encryptionFactory.Enrypt(loginAdto.AccessToken, _jwtIssuerOptions.SigningKey);

	        return jwt;
	    }
    }
}

