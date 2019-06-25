using System;
using ApplicationManager.Domain.Identities;
using Common.Authorisation;

namespace ApplicationManager.ApplicationServices.Identities.Authorisation
{
    public class IdentityAuthorisationContext : IAuthorisationContext
    {
        public IdentityAuthorisationContext(Guid id, string action)
        {
            ResourceId = id;
            Action = action;
        }

        public Type ResourceType => typeof(Identity);

        public Guid? ResourceId { get; }

        public string Resource => AuthorisationResource.Identity;

        public string Action { get; }

        public static IdentityAuthorisationContext Create(Guid id, string action)
        {
            return new IdentityAuthorisationContext(id, action);
        }
    }
}