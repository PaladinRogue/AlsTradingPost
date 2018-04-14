using System;
using Common.Domain.Concurrency;

namespace AlsTradingPost.Domain.AdminDomain.Models
{
    public class UpdateAdminDdto : VersionedDdto
    {
        public Guid Id { get; set; }
    }
}
