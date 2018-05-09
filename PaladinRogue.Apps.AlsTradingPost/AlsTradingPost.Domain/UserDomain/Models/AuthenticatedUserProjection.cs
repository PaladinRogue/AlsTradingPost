using System;
using AlsTradingPost.Resources;
using Common.Domain.Concurrency;

namespace AlsTradingPost.Domain.UserDomain.Models
{
    public class AuthenticatedUserProjection : VersionedProjection
    {
        public Guid Id { get; set; }
        public PersonaFlags Personas { get; set; }
    }
}
