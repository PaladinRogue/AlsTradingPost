using System;
using Common.Domain.Models;

namespace Authentication.Domain.IdentityServices.Models
{
    public class IdentityProjection : VersionedProjection
    {
        public Guid Id { get; set; }
    }
}
