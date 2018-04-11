using System;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.UserServices.Models
{
    public class UpdateUserDdto : VersionedDdto
    {
        public Guid Id { get; set; }
    }
}
