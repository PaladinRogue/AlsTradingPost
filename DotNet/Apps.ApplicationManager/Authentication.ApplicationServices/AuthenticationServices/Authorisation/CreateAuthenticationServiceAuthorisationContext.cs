using System;
using Authentication.Domain.Identities;
using Authorisation.Application;
using Authorisation.Application.Contexts;

namespace Authentication.ApplicationServices.AuthenticationServices.Authorisation
{
    public class CreateAuthenticationServiceAuthorisationContext : IAuthorisationContext
    {
        public CreateAuthenticationServiceAuthorisationContext()
        {
        }

        public CreateAuthenticationServiceAuthorisationContext(Guid id)
        {
            ResourceId = id;
        }

        public Type ResourceType => typeof(Identity);

        public Guid? ResourceId { get; }

        public string Resource => AuthorisationResource.AuthenticationService;

        public string Action => AuthorisationAction.Create;

        public static CreateAuthenticationServiceAuthorisationContext Create(Guid id)
        {
            return new CreateAuthenticationServiceAuthorisationContext(id);
        }
    }
}