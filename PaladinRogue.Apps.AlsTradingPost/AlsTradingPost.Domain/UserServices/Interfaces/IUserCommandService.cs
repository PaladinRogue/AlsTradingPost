using AlsTradingPost.Domain.UserServices.Models;
using Common.Domain.Interfaces;

namespace AlsTradingPost.Domain.UserServices.Interfaces
{
    public interface IUserCommandService : ICreateCommandService<CreateUserDdto, UserProjection>, IUpdateCommandService<UpdateUserDdto, UserProjection>
    {
    }
}
