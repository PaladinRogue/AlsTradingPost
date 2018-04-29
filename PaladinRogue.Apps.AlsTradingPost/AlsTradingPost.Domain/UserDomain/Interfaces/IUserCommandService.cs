using AlsTradingPost.Domain.UserDomain.Models;
using Common.Domain.Services.Interfaces;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserCommandService : IUpdateCommandService<UpdateUserDdto, UserProjection>, ICreateCommandService<CreateUserDdto, UserProjection>
    {
    }
}
