using System;

namespace PaladinRogue.Library.Core.Api.Resources
{
    public interface IEntityResource : IResource
    {
        Guid Id { get; set; }
    }
}