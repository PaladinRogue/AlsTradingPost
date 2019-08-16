using System;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Library.Authorisation.Common.Contexts;

namespace PaladinRogue.Authentication.Application.Identities.Authorisation
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