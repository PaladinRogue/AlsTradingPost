using System;
using Common.Application.Models;

namespace Authentication.Application.Identity.Models
{
    public class UpdateIdentityAdto : InboundVersionedAdto
    {
        public Guid Id { get; set; }
        public string AuthenticationId { get; set; }
    }
}