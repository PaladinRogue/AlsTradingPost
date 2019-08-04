using System;

namespace Common.Authorisation.Contexts
{
    public interface IAuthorisationContext : IAuthorisationRule
    {
        Type ResourceType { get; }

        Guid? ResourceId { get; }
    }
}