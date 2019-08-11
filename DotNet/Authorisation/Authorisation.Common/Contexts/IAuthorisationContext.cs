using System;
using PaladinRogue.Libray.Authorisation.Common.Rules;

namespace PaladinRogue.Libray.Authorisation.Common.Contexts
{
    public interface IAuthorisationContext : IAuthorisationRule
    {
        Type ResourceType { get; }

        Guid? ResourceId { get; }
    }
}