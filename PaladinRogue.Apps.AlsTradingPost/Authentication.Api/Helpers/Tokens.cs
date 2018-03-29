using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.Api.Resources;
using Common.Api.Authentication;

namespace Authentication.Api.Helpers
{
    public static class Tokens
	{
		public static async Task<JwtResource> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions)
		{
			return new JwtResource {
				AuthToken = await jwtFactory.GenerateEncodedToken(identity),
				ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds
			};
		}
	}
}
