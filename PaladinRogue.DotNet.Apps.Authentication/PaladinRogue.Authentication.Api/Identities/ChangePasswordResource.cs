using System;
using PaladinRogue.Library.Core.Api.Resources;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.Password)]
    public class ChangePasswordResource : IEntityResource
    {
        public Guid Id { get; set; }
    }
}