using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Authentication.Api.Factories.Interfaces;
using Common.Api.Authentication;
using Common.Api.Factories.Interfaces;
using Microsoft.Extensions.Options;
using JwtConstants = Common.Api.Authentication.JwtConstants;

namespace Authentication.Api.Factories
{
    public class ClaimsFactory : IClaimsFactory
    {
	    private readonly IEncryptionFactory _encryptionFactory;
	    private readonly JwtIssuerOptions _jwtIssuerOptions;
		
		public ClaimsFactory(IEncryptionFactory encryptionFactory, IOptions<JwtIssuerOptions> jwtIssuerOptionsAccessor)
		{
			_encryptionFactory = encryptionFactory;
			_jwtIssuerOptions = jwtIssuerOptionsAccessor.Value;
		}

	    public ClaimsIdentity GenerateClaimsIdentity(Guid id, string accessToken)
	    {
		    return new ClaimsIdentity(new GenericIdentity(id.ToString(), "Token"), new[]
		    {
			    new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
			    new Claim(JwtConstants.Strings.ClaimIdentifiers.AccessToken, _encryptionFactory.Enrypt(accessToken, _jwtIssuerOptions.SigningKey))
		    });
	    }
    }
}
