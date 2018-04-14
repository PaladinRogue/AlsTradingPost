using System;
using AlsTradingPost.Resources;
using Common.Domain.Concurrency;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.UserServices.Models
{
    public class UserProjection : VersionedProjection
    {
        public Guid Id { get; set; }
        public Guid IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public Persona Personas { get; set; }
    }
}
