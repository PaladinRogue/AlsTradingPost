using System;

namespace PaladinRogue.Libray.Core.Api.Resources
{
    public interface IEntityResource : IResource
    {
        Guid Id { get; set; }
    }
}