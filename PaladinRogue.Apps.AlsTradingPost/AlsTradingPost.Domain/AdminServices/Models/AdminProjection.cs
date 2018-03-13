using System;
using Common.Domain.Interfaces;

namespace AlsTradingPost.Domain.AdminServices.Models
{
    public class AdminProjection : IVersionedProjection
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Version { get; set; }
    }
}
