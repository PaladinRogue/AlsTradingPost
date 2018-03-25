using System;
using Common.Application.Models;

namespace Authentication.Application.Identity.Models
{
    public class IdentityAdto : OutboundVersionedAdto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public Guid SecurityStamp { get; set; }
        public string AuthenticationId { get; set; }
    }
}
