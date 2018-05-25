using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.UserDomain.Models;
using Common.Domain.Services.Query;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserQueryService : ICheckExistsService, IGetByIdQueryService<User>
    {
        User GetByIdentityId(Guid identityId);

        IEnumerable<UserPersonaProjection> GetUserPersonas(Guid userid);
    }
}
