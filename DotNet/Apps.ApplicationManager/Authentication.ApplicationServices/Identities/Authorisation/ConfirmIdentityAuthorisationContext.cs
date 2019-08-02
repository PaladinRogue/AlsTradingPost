using System;
using Authentication.Domain.Identities;
using Common.Authorisation.Contexts;

namespace Authentication.ApplicationServices.Identities.Authorisation
{
    public class ConfirmIdentityAuthorisationContext : IAuthorisationContext
    {
        public ConfirmIdentityAuthorisationContext()
        {
        }

        public ConfirmIdentityAuthorisationContext(Guid id)
        {
            ResourceId = id;
        }

        public string Resource => AuthorisationResource.Identity;

        public string Action => IdentityAuthorisationAction.Confirm;

        public Type ResourceType => typeof(Identity);

        public Guid? ResourceId { get; }
    }
}