using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.UserDomain.Models;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserQueryService
    {
        UserProjection GetByIdentityId(Guid identityId);

        IEnumerable<UserPersonaProjection> GetUserPersonas(Guid userid);
    }
}
