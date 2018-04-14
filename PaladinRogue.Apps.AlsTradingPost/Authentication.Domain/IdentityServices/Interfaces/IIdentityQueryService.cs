using Authentication.Domain.IdentityServices.Models;
using Common.Domain.Services;

namespace Authentication.Domain.IdentityServices.Interfaces
{
    public interface IIdentityQueryService : IQueryService<IdentityProjection>
    {
	    IdentityProjection GetByAuthenticationId(string authenticationId);
    }
}
