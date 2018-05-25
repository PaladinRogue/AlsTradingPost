using AlsTradingPost.Domain.Models;
using Common.Domain.Services.Command;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserCommandService : IUpdateCommandService<User>, ICreateCommandService<User>
    {
    }
}
