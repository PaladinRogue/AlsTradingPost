using Authentication.Domain.IdentityServices.Models;
using Common.Domain.Services;
using Common.Domain.Services.Interfaces;

namespace Authentication.Domain.IdentityServices.Interfaces
{
    public interface IIdentityQueryService : IQueryService<IdentityProjection>
    {
	    IdentityProjection GetByAuthenticationId(string authenticationId);
    }
}
