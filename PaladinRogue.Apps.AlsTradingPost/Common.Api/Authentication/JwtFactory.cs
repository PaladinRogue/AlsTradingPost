﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Common.Api.Authentication
{
	public class JwtFactory : IJwtFactory
	{
		private readonly JwtIssuerOptions _jwtOptions;

		public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
		{
			_jwtOptions = jwtOptions.Value;
			ThrowIfInvalidOptions(_jwtOptions);
		}

		public async Task<string> GenerateEncodedToken(ClaimsIdentity identity)
		{
			var claims = new[]
			{
				identity.FindFirst(JwtRegisteredClaimNames.Sub),
				new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
				new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
				identity.FindFirst(JwtConstants.Strings.ClaimIdentifiers.AccessToken)
			};

			// Create the JWT security token and encode it.
			var jwt = new JwtSecurityToken(
				issuer: _jwtOptions.Issuer,
				audience: _jwtOptions.Audience,
				claims: claims,
				notBefore: _jwtOptions.NotBefore,
				expires: _jwtOptions.Expiration,
				signingCredentials: _jwtOptions.SigningCredentials);

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

			if (options.SigningCredentials == null)
			{
				throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
			}

			if (options.JtiGenerator == null)
			{
				throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
			}
		}
	}
}

