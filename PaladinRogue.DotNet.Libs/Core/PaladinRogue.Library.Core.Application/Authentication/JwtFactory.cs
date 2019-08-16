using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace PaladinRogue.Library.Core.Application.Authentication
{
	public class JwtFactory : IJwtFactory
	{
		private readonly JwtIssuerOptions _jwtIssuerOptions;

		public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
		{
		    _jwtIssuerOptions = jwtOptions.Value;

			ThrowIfInvalidOptions(_jwtIssuerOptions);
	    }

	    public async Task<T> GenerateJwtAsync<T>(ClaimsIdentity identity, Guid sessionId) where T : IJwtAdto
	    {
	        T jwt = Activator.CreateInstance<T>();

	        jwt.AuthToken = await GenerateEncodedToken(identity);
	        jwt.ExpiresIn = (int) _jwtIssuerOptions.ValidFor.TotalSeconds;
	        jwt.SessionId = sessionId;

	        return jwt;
	    }

        private async Task<string> GenerateEncodedToken(ClaimsIdentity identity)
		{
			IEnumerable<Claim> claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Jti, await _jwtIssuerOptions.JtiGenerator()),
				new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64)
            }.Concat(identity.Claims);

		    JwtSecurityToken jwt = new JwtSecurityToken(
		        issuer: _jwtIssuerOptions.Issuer,
		        audience: _jwtIssuerOptions.Audience,
		        claims: claims,
		        notBefore: _jwtIssuerOptions.NotBefore,
		        expires: _jwtIssuerOptions.Expiration,
		        signingCredentials: _jwtIssuerOptions.SigningCredentials);

			string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

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

