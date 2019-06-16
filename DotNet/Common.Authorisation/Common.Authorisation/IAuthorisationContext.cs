using System;

namespace Common.Authorisation
{
    public interface IAuthorisationContext : IAuthorisationRule
    {
        Type ResourceType { get; }

        Guid? ResourceId { get; }
    }
}