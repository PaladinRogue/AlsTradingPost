using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Api.Authentication.Constants;
using Common.Api.Factories.Interfaces;
using Common.Api.Resource.Interfaces;
using Microsoft.Extensions.Options;

namespace Common.Api.Authentication
{
	public class JwtFactory : IJwtFactory
	{
		private readonly JwtIssuerOptions _jwtIssuerOptions;
		private readonly IClaimsFactory _claimsFactory;

		public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions, IClaimsFactory claimsFactory)
		{
		    _claimsFactory = claimsFactory;
		    _jwtIssuerOptions = jwtOptions.Value;

			ThrowIfInvalidOptions(_jwtIssuerOptions);
	    }

	    public async Task<T> GenerateJwt<T>(Guid id) where T : IJwtResource
	    {
	        T jwt = Activator.CreateInstance<T>();

	        jwt.AuthToken = await GenerateEncodedToken(_claimsFactory.GenerateClaimsIdentity(id));
	        jwt.ExpiresIn = (int) _jwtIssuerOptions.ValidFor.TotalSeconds;

	        return jwt;
	    }

        private async Task<string> GenerateEncodedToken(ClaimsIdentity identity)
		{
			var claims = new[]
			{
				identity.FindFirst(JwtRegisteredClaimNames.Sub),
				new Claim(JwtRegisteredClaimNames.Jti, await _jwtIssuerOptions.JtiGenerator()),
				new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst(JwtClaimIdentifiers.Rol)
            };

			// Create the JWT security token and encode it.
		    var jwt = new JwtSecurityToken(
		        issuer: _jwtIssuerOptions.Issuer,
		        audience: _jwtIssuerOptions.Audience,
		        claims: claims,
		        notBefore: _jwtIssuerOptions.NotBefore,
		        expires: _jwtIssuerOptions.Expiration,
		        signingCredentials: _jwtIssuerOptions.SigningCredentials);

			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			return encodedJwt;
		}

		/// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
		private static long ToUnixEpochDate(DateTime date)
			=> (long) Math.Round((date.ToUniversalTime() -
			                      new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
				.TotalSeconds);

		private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));

			if (options.ValidFor <= TimeSpan.Zero)
			{
				throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
			}

			if (options.SigningKey == null)
			{
				throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningKey));
			}

			if (options.JtiGenerator == null)
			{
				throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
			}
		}
	}
}

