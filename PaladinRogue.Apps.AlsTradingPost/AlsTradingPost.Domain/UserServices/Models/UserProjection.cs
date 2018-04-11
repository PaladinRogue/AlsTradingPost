using System;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.UserServices.Models
{
    public class UserProjection : VersionedProjection
    {
        public Guid Id { get; set; }
    }
}
