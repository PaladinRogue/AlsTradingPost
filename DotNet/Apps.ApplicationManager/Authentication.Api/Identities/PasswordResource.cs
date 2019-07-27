using System;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.Password)]
    [Link(LinkDictionary.ChangePassword, RouteDictionary.ChangePasswordResourceTemplate, HttpVerb.Get)]
    public class PasswordResource : IEntityResource
    {
        [Ignore]
        public Guid Id { get; set; }
    }
}