using System;
using ApplicationManager.Domain.Identities;
using Common.Authorisation;
using Common.Authorisation.Contexts;

namespace ApplicationManager.ApplicationServices.AuthenticationServices.Authorisation
{
    public class GetAuthenticationServiceAuthorisationContext : IAuthorisationContext
    {
        public GetAuthenticationServiceAuthorisationContext()
        {
        }

        public GetAuthenticationServiceAuthorisationContext(Guid id)
        {
            ResourceId = id;
        }

        public Type ResourceType => typeof(Identity);

        public Guid? ResourceId { get; }

        public string Resource => AuthorisationResource.AuthenticationService;

        public string Action => AuthorisationAction.Get;

        public static GetAuthenticationServiceAuthorisationContext Create(Guid id)
        {
            return new GetAuthenticationServiceAuthorisationContext(id);
        }
    }
}