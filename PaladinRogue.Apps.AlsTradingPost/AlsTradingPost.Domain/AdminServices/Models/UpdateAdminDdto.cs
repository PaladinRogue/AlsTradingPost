using System;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.AdminServices.Models
{
    public class UpdateAdminDdto : VersionedDdto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
