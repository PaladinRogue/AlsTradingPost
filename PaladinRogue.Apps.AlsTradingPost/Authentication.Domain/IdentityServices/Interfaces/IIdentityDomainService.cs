using Authentication.Domain.IdentityServices.Models;

namespace Authentication.Domain.IdentityServices.Interfaces
{
    public interface IIdentityDomainService
    {
        AuthenticatedIdentityProjection Login(LoginDdto loginDdto);
    }
}