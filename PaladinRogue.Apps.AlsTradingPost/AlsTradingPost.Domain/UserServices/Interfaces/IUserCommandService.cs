﻿using AlsTradingPost.Domain.UserServices.Models;
using Common.Domain.Services;

namespace AlsTradingPost.Domain.UserServices.Interfaces
{
    public interface IUserCommandService : IUpdateCommandService<UpdateUserDdto, UserProjection>, ICreateCommandService<CreateUserDdto, UserProjection>
    {
    }
}
