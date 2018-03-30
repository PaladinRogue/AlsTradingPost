using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.Api.Authentication
{
    public interface IJwtFactory
	{
		Task<string> GenerateEncodedToken(ClaimsIdentity identity);
	}
}
