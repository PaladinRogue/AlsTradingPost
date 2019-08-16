using System.Collections.Generic;
using PaladinRogue.Library.Core.Api.Resources;

namespace PaladinRogue.Authentication.Api.AuthenticationService
{
    public class AuthenticationServiceTypesResource : ICollectionResource<AuthenticationServiceTypeResource>
    {
        public IList<AuthenticationServiceTypeResource> Results { get; set; }
    }
}