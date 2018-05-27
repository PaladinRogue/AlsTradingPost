using System;

namespace Common.Application.Authorisation
{
    public interface IAuthorisationContext
    {
        Type ResourceType { get; }

        Guid? ResourceId { get; }
    }
}
