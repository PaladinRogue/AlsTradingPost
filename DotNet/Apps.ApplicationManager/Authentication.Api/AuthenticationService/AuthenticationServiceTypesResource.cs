using System.Collections.Generic;
using PaladinRogue.Libray.Core.Api.Resources;

namespace PaladinRogue.Authentication.Api.AuthenticationService
{
    public class AuthenticationServiceTypesResource : ICollectionResource<AuthenticationServiceTypeResource>
    {
        public IList<AuthenticationServiceTypeResource> Results { get; set; }
    }
}