using AlsTradingPost.Domain.UserDomain.Models;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserDomainService
    {
        UserProjection Login(LoginDdto loginDdto);
    }
}