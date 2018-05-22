using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.UserDomain.Models;
using Common.Domain.Services.Interfaces;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserQueryService : ICheckExistsService
    {
        UserProjection GetByIdentityId(Guid identityId);

        IEnumerable<UserPersonaProjection> GetUserPersonas(Guid userid);
    }
}
