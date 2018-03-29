using System;
using Common.Domain.Models;

namespace Authentication.Domain.IdentityServices.Models
{
    public class UpdateIdentityDdto : VersionedDdto
    {
        public Guid Id { get; set; }
        public string AuthenticationId { get; set; }
    }
}
