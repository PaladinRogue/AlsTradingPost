using System;
using Common.Api.Resources;

namespace Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.Password)]
    public class ChangePasswordResource : IEntityResource
    {
        [Ignore]
        public Guid Id { get; set; }
    }
}