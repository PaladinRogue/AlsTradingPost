using System;
using Authentication.Domain.Identities;
using Authorisation.Application;
using Authorisation.Application.Contexts;

namespace Authentication.Application.Identities.Authorisation
{
    public class CreateRefreshTokenAuthorisationContext : IAuthorisationContext
    {
        public CreateRefreshTokenAuthorisationContext()
        {
        }

        public CreateRefreshTokenAuthorisationContext(Guid id)
        {
            ResourceId = id;
        }

        public string Resource => AuthorisationResource.Identity;

        public string Action => AuthorisationAction.Update;

        public Type ResourceType => typeof(Identity);

        public Guid? ResourceId { get; }
    }
}