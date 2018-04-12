using System;
using AlsTradingPost.Domain.UserServices.Models;
using Common.Domain.Interfaces;

namespace AlsTradingPost.Domain.UserServices.Interfaces
{
    public interface IUserQueryService : IQueryService<UserProjection>, ISummaryQueryService<UserSummaryProjection>
    {
        UserProjection GetByIdentityId(Guid identityId);
    }
}
