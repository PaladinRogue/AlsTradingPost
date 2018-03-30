using System.Security.Claims;
using System.Threading.Tasks;
using Common.Api.Authentication;
using Common.Api.Resource;

namespace Common.Api.Helpers
{
    public static class Tokens
	{
		public static async Task<JwtResource> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, JwtIssuerOptions jwtOptions)
		{
			return new JwtResource {
				AuthToken = await jwtFactory.GenerateEncodedToken(identity),
				ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds
			};
		}
	}
}
