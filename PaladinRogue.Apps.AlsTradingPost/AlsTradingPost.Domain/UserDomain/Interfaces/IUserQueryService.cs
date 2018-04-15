using System;
using AlsTradingPost.Domain.UserDomain.Models;
using Common.Domain.Services.Interfaces;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserQueryService : IQueryService<UserProjection>
    {
        UserProjection GetByIdentityId(Guid identityId);
    }
}
