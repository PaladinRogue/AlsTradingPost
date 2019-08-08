using System;
using Authentication.Domain.Identities;
using Authorisation.Application;
using Authorisation.Application.Contexts;

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