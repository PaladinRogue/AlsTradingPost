using System;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Library.Authorisation.Common;
using PaladinRogue.Library.Authorisation.Common.Contexts;

namespace PaladinRogue.Authentication.Application.Identities.Authorisation
{
    public class ChangePasswordAuthorisationContext : IAuthorisationContext
    {
        public ChangePasswordAuthorisationContext()
        {
        }

        public ChangePasswordAuthorisationContext(Guid id)
        {
            ResourceId = id;
        }

        public string Resource => AuthorisationResource.Identity;

        public string Action => AuthorisationAction.Update;

        public Type ResourceType => typeof(Identity);

        public Guid? ResourceId { get; }
    }
}