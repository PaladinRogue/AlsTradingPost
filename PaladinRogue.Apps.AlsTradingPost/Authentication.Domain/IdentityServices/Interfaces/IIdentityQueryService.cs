using Authentication.Domain.IdentityServices.Models;
using Common.Domain.Services.Interfaces;

namespace Authentication.Domain.IdentityServices.Interfaces
{
    public interface IIdentityQueryService : IGetByIdService<IdentityProjection>
    {
	    IdentityProjection GetByAuthenticationId(string authenticationId);
    }
}
