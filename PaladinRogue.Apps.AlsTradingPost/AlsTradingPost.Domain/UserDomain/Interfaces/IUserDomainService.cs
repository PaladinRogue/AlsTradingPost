using AlsTradingPost.Domain.UserDomain.Models;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserDomainService
    {
        AuthenticatedUserProjection Login(LoginDdto loginDdto);
    }
}