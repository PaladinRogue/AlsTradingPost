using System;
using AlsTradingPost.Domain.Models;
using Common.Domain.Services.Query;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserQueryService : ICheckExistsService, IGetByIdQueryService<User>
    {
        User GetByIdentityId(Guid identityId);
    }
}
