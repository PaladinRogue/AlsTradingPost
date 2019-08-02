using System.Collections.Generic;
using Common.Api.Resources;

namespace Authentication.Api.AuthenticationService
{
    public class AuthenticationServiceTypesResource : ICollectionResource<AuthenticationServiceTypeResource>
    {
        public IList<AuthenticationServiceTypeResource> Results { get; set; }
    }
}