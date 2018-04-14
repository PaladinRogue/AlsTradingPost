using System;
using AlsTradingPost.Domain.UserDomain.Models;
using Common.Domain.Services;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserQueryService : IQueryService<UserProjection>, ISummaryQueryService<UserSummaryProjection>
    {
        UserProjection GetByIdentityId(Guid identityId);
    }
}
