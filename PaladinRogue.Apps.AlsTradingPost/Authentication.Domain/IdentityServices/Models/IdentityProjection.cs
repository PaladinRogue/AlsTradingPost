using System;
using Common.Domain.Concurrency;
using Common.Domain.Models;

namespace Authentication.Domain.IdentityServices.Models
{
    public class IdentityProjection : VersionedProjection
    {
        public Guid Id { get; set; }
        public string AuthenticationId { get; set; }
    }
}
