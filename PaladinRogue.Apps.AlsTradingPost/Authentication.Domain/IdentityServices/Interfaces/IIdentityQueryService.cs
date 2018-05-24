using Authentication.Domain.Models;
using Common.Domain.Services.Domain;

namespace Authentication.Domain.IdentityServices.Interfaces
{
    public interface IIdentityQueryService : IGetByIdService<Identity>
    {
	    Identity GetByAuthenticationId(string authenticationId);
    }
}
