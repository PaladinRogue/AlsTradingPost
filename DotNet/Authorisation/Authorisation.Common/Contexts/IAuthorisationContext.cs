using System;

namespace Authorisation.Application.Contexts
{
    public interface IAuthorisationContext : IAuthorisationRule
    {
        Type ResourceType { get; }

        Guid? ResourceId { get; }
    }
}