using System;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Library.Authorisation.Common;
using PaladinRogue.Library.Authorisation.Common.Contexts;

namespace PaladinRogue.Authentication.Application.AuthenticationServices.Authorisation
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