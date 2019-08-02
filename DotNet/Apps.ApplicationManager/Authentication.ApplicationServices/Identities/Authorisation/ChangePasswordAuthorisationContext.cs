using System;
using Authentication.Domain.Identities;
using Common.Authorisation;
using Common.Authorisation.Contexts;

namespace Authentication.ApplicationServices.Identities.Authorisation
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