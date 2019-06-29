using System;
using Common.ApplicationServices.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace ApplicationManager.ApplicationServices.Identities.Models
{
    public class ConfirmIdentityAdto
    {
        public Guid IdentityId { get; set; }

        public string Token { get; set; }
    }
}