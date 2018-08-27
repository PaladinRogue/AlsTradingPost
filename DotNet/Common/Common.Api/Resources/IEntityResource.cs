using System;

namespace Common.Api.Resources
{
    public interface IEntityResource : IResource
    {
        Guid Id { get; set; }
    }
}